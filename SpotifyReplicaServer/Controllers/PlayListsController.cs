using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyReplicaServer.Abstraction;
using System;

namespace SpotifyReplicaServer.Controllers
{
    public class PlayListsController : ControllerBase
    {
        private readonly IPlayListService playListService;

        public PlayListsController(IPlayListService playListService)
        {
            this.playListService = playListService ?? throw new ArgumentNullException(nameof(playListService));
        }
    }
}
