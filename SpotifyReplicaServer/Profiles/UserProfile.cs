using AutoMapper;
using SpotifyReplicaServer.Data.Models;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Transfer;

namespace SpotifyReplicaServer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserContext>();
            CreateMap<UserContext, User>();
        }
    }
}
