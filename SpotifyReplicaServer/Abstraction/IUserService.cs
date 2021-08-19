using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyReplicaServer.Abstraction
{
    public interface IUserService
    {
        string Login(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        Task<string> Register(User user);
        void SetUser(User user);
        User GetUser();
        Task<bool> Alter(User user);
    }
}
