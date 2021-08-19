using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpotifyReplicaServer.Abstraction;
using SpotifyReplicaServer.Data;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyReplicaServer.Shared.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings appSettings;
        private readonly SpotifyReplicaServerDbContext dbContext;
        private User user;

        public UserService(IOptions<AppSettings> appSettings, SpotifyReplicaServerDbContext dbContext)
        {
            this.appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings)); ;
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public string Login(AuthenticateRequest model)
        {
            var user = dbContext.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return null;

            var token = generateJwtToken(user);

            return token;
        }

        public async Task<string> Register(User user)
        {
            if (dbContext.Users.ToList().Contains(user))
            {
                return null;
            }

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            var token = generateJwtToken(user);

            return token;
        }


        public async Task<bool> Alter(User user)
        {
            var isChanged = false;
            var userTmp = dbContext.Users.FirstOrDefault(x => x.Id == user.Id);

            if (user.LastName != null)
            {
                isChanged = true;
                userTmp.LastName = user.LastName;
            }

            if (user.FirstName != null)
            {
                isChanged = true;
                userTmp.FirstName = user.FirstName;
            }

            if (user.Email != null)
            {
                isChanged = true;
                userTmp.Email = user.Email;
            }

            if (user.Password != null)
            {
                isChanged = true;
                userTmp.Password = user.Password;
            }

            if (user.Username != null)
            {
                isChanged = true;
                userTmp.Username = user.Username;
            }

            if (user.Avatar != null)
            {
                isChanged = true;
                userTmp.Avatar = user.Avatar;
            }

            if (user.Gender != null)
            {
                isChanged = true;
                userTmp.Gender = user.Gender;
            }

            if (user.BirthDate != null)
            {
                isChanged = true;
                userTmp.BirthDate = user.BirthDate;
            }

            if (isChanged)
            {
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            return isChanged;
        }

        public IEnumerable<User> GetAll()
        {
            return dbContext.Users.AsEnumerable<User>();
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
            return dbContext.Users.FirstOrDefault(x => x.Id == id);
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
