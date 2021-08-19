using Microsoft.AspNetCore.Mvc;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using System.Collections.Generic;

namespace SpotifyReplicaServer.Controllers
{
    [ApiController]
    [Route("songs")]
    public class SongsController
    {
        [HttpGet]
        public List<Song> GetSongs()
        {
            return null;
        }

        [HttpGet("{ id }")]
        public Song GetSong(int id)
        {
            return null;
        }

        [HttpPost("page")]
        public List<Song> GetPagedSongs(PagingInformationRequest pagingInformation)
        {
            return null;
        }

        [HttpPost("find")]
        public List<Song> FindSongs(FindingSongInformationRequest findingSongInformation)
        {
            return null;
        }
    }
}
