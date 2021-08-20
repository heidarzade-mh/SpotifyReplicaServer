using AutoMapper;
using SpotifyReplicaServer.Data.Models;
using SpotifyReplicaServer.Models;

namespace SpotifyReplicaServer.Profiles
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<GenderContext, Gender>();
            CreateMap<Gender, GenderContext>();
        }
    }
}
