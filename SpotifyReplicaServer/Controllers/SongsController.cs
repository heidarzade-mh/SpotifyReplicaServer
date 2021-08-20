using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("{id}")]
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
        public IActionResult GetPagedSongs(PagingInformationRequest pagingInformation)
        {
            var songs = this.songsService.GetPagedSongs(pagingInformation);

            if (songs == null)
            {
                return BadRequest(new { message = "مقادیر وارد شده معتبر نمی‌باشد و یا نتیجه‌ای وجود ندارد." });
            }

            return Ok(songs);
        }

        [HttpPost("find")]
        public List<Song> FindSongs(FindingSongInformationRequest findingSongInformation)
        {
            return this.songsService.FindSongs(findingSongInformation);
        }

        /*[HttpPost("add")]
        public void AddSong(List<Song> songs)
        {
            songs.ForEach(son => son.Id = null);

            var response = this.songsService.AddSongs(songs);

            Console.WriteLine(response.Result);
        }*/
    }
}
