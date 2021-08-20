using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyReplicaServer.Abstraction
{
    public interface IPlayListService
    {
        PlayList GetPlayList(int id);
        List<PlayList> GetPlayLists();
        Task<PlayList> Create(PlayList playList);
        Task<string> Remove(int id);
        Task<string> RemoveSong(PlayListSongInformationRequest songInformationRequest);
        Task<string> AddSong(PlayListSongInformationRequest songInformationRequest);
    }
}
