using AutoMapper;
using SpotifyReplicaServer.Data.Models;
using SpotifyReplicaServer.Models;

namespace SpotifyReplicaServer.Profiles
{
    public class PlayListProfile : Profile
    {
        public PlayListProfile()
        {
            CreateMap<PlayListContext, PlayList>();
            CreateMap<PlayList, PlayListContext>();
        }
    }
}
