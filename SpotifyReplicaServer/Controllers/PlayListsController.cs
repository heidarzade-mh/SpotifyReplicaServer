using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpotifyReplicaServer.Abstraction;
using SpotifyReplicaServer.Business.Athentication;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using SpotifyReplicaServer.Models.Transfer;
using System;
using System.Collections.Generic;

namespace SpotifyReplicaServer.Controllers
{
    [ApiController]
    [Route("playLists")]
    public class PlayListsController : ControllerBase
    {
        private readonly IPlayListService playListService;
        private readonly IMapper mapper;

        public PlayListsController(IPlayListService playListService, IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.playListService = playListService ?? throw new ArgumentNullException(nameof(playListService));
        }

        [Authorize]
        [HttpGet]
        [Route("{id?}")]
        public IActionResult GetPlayList(int id)
        {
            var result = this.playListService.GetPlayList(id);
            if (result == null)
            {
                return BadRequest(new { message = "شناسه مورد نظر یافت نشد."});
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public List<PlayList> GetPlayLists()
        {
            var playLists = this.playListService.GetPlayLists();
            return playLists;
        }

        [Authorize]
        [HttpPost("create")]
        public PlayList Create(PlayListViewModel playListViewModel)
        {
            var playList = mapper.Map<PlayList>(playListViewModel);
            return this.playListService.Create(playList).Result;
        }

        [Authorize]
        [HttpDelete]
        [Route("remove/{id?}")]
        public IActionResult Remove(int id)
        {
            return Ok(new { message = this.playListService.Remove(id).Result });
        }

        [Authorize]
        [HttpPost("add-song")]
        public IActionResult AddSong(PlayListSongInformationRequest
            songInformationRequest)
        {
            return Ok(new { message = this.playListService.AddSong(songInformationRequest).Result });
        }

        [Authorize]
        [HttpDelete("remove-song")]
        public IActionResult RemoveSong(PlayListSongInformationRequest
            songInformationRequest)
        {
            return Ok(new { message = this.playListService.RemoveSong(songInformationRequest).Result });
        }
    }
}
