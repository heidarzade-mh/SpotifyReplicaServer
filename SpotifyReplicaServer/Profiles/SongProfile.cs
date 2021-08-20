using AutoMapper;
using SpotifyReplicaServer.Data.Models;
using SpotifyReplicaServer.Models;

namespace SpotifyReplicaServer.Profiles
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<SongContext, Song>();
            CreateMap<Song, SongContext>();
        }
    }
}
