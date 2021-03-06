using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyReplicaServer.Abstraction
{
    public interface ISongsService
    {
        List<Song> FindSongs(FindingSongInformationRequest findingSongInformation);
        List<Song> GetPagedSongs(PagingInformationRequest pagingInformation);
        Song GetSong(int id);
        List<Song> GetSongs();
        Task<string> AddSongs(List<Song> songs);
    }
}