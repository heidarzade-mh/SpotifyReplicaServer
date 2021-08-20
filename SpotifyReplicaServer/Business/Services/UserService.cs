using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpotifyReplicaServer.Abstraction;
using SpotifyReplicaServer.Data;
using SpotifyReplicaServer.Data.Models;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyReplicaServer.Business.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings appSettings;
        private readonly DatabaseContext dbContext;
        private readonly IMapper mapper;
        private User user;

        public UserService(IOptions<AppSettings> appSettings, DatabaseContext dbContext, IMapper mapper)
        {
            this.appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public string Login(AuthenticateRequest model)
        {
            var userContext = dbContext.Users.SingleOrDefault(x => (x.Username == model.Username || x.Email == model.Username) && x.Password == model.Password);
            var user = mapper.Map<User>(userContext);
            if (user == null) return null;

            var token = generateJwtToken(user);

            return token;
        }

        public async Task<string> Register(User user)
        {
            var usersContext = dbContext.Users.ToList();
            var usersTemp = mapper.Map<List<User>>(usersContext);
            if (usersTemp.Contains(user))
            {
                return null;
            }

            var userContextTemp = mapper.Map<UserContext>(user);
            dbContext.Users.Add(userContextTemp);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            var token = generateJwtToken(user);

            return token;
        }


        public async Task<string> Alter(User user)
        {
            var isChanged = false;
            var userContextTmp = dbContext.Users.FirstOrDefault(x => x.Id == this.user.Id);
            var usersContext = dbContext.Users.ToList();
            var users = mapper.Map<List<User>>(usersContext);

            if (users.Contains(user)) {
                return "شناسه‌کاربری و یا ایمیل تکراری می‌باشد.";
            }

            if (user.LastName != null)
            {
                isChanged = true;
                userContextTmp.LastName = user.LastName;
            }

            if (user.FirstName != null)
            {
                isChanged = true;
                userContextTmp.FirstName = user.FirstName;
            }

            if (user.Email != null)
            {
                isChanged = true;
                userContextTmp.Email = user.Email;
            }

            if (user.Password != null)
            {
                isChanged = true;
                userContextTmp.Password = user.Password;
            }

            if (user.Username != null)
            {
                isChanged = true;
                userContextTmp.Username = user.Username;
            }

            if (user.Avatar != null)
            {
                isChanged = true;
                userContextTmp.Avatar = user.Avatar;
            }

            if (user.Gender != 0)
            {
                isChanged = true;
                userContextTmp.Gender = (GenderContext)(int) user.Gender;
            }

            if (user.BirthDate != null)
            {
                isChanged = true;
                userContextTmp.BirthDate = user.BirthDate;
            }

            if (isChanged)
            {
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            } 
            else
            {
                return "وارد کردن حداقل یک مشخصه الزامیست.";
            }

            return "";
        }

        public IEnumerable<User> GetAll()
        {
            var usersContext = dbContext.Users.AsEnumerable<UserContext>();
            var users = mapper.Map<List<User>>(usersContext);
            return users;
        }

        public User GetUser()
        {
            return this.user;
        }

        public void SetUser(User user)
        {
            this.user = user;
        }

        public User GetById(int id)
        {
            var userContext = dbContext.Users.FirstOrDefault(x => x.Id == id);
            return mapper.Map<User>(userContext);
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
