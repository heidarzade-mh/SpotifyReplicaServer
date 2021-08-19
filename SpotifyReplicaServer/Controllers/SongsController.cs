using Microsoft.AspNetCore.Mvc;
using SpotifyReplicaServer.Abstraction;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using System;
using System.Collections.Generic;

namespace SpotifyReplicaServer.Controllers
{
    [ApiController]
    [Route("songs")]
    public class SongsController : ControllerBase
    {
        private readonly ISongsService songsService;
        public SongsController(ISongsService songsService)
        {
            this.songsService = songsService ?? throw new ArgumentNullException(nameof(songsService));
        }

        [HttpGet]
        public List<Song> GetSongs()
        {
            return this.songsService.GetSongs();
        }

        [HttpGet("{ id }")]
        public IActionResult GetSong(int id)
        {
            var song = this.songsService.GetSong(id);
            if (song == null)
            {
                return NotFound(song);
            }

            return Ok(this.songsService.GetSong(id));
        }

        [HttpPost("page")]
        public List<Song> GetPagedSongs(PagingInformationRequest pagingInformation)
        {
            return this.songsService.GetPagedSongs(pagingInformation);
        }

        [HttpPost("find")]
        public List<Song> FindSongs(FindingSongInformationRequest findingSongInformation)
        {
            return this.songsService.FindSongs(findingSongInformation);
        }
    }
}
