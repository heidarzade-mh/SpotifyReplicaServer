using AutoMapper;
using SpotifyReplicaServer.Data.Models;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Transfer;

namespace SpotifyReplicaServer.Profiles
{
    public class PlayListProfile : Profile
    {
        public PlayListProfile()
        {
            CreateMap<PlayListContext, PlayList>();
            CreateMap<PlayList, PlayListContext>();

            CreateMap<PlayListViewModel, PlayList>();
            CreateMap<PlayList, PlayListViewModel>();
        }
    }
}
