using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpotifyReplicaServer.Abstraction;
using SpotifyReplicaServer.Data;
using SpotifyReplicaServer.Data.Models;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyReplicaServer.Business.Services
{
    public class PlayListService : IPlayListService
    {
        private readonly DatabaseContext dbContext;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserContext user;

        public PlayListService(DatabaseContext dbContext, IUserService userService, IMapper mapper)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.user = this.dbContext.Users
                .Include(u => u.PlayLists)
                .ThenInclude(p => p.Songs)
                .FirstOrDefault(uc => uc.Id == this.userService.GetUser().Id);
        }

        public async Task<string> AddSong(PlayListSongInformationRequest songInformationRequest)
        {
            var playListContext = this.user.PlayLists.FirstOrDefault
                (x => x.Id == songInformationRequest.PlayListId);

            if (playListContext == null)
            {
                return "شناسه مورد نظر یافت نشد.";
            }

            var song = this.dbContext.Songs.FirstOrDefault(x => x.Id == songInformationRequest.SongId);
            if (song == null)
            {
                return "شناسه مورد نظر یافت نشد.";
            }

            if (playListContext.Songs.Contains(song))
            {
                return "این آهنگ قبلا اضافه شده است.";
            }

            playListContext.Songs.Add(song);

            await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

            return "عملیات با موفقیت انجام شد.";
        }

        public async Task<PlayList> Create(PlayList playList)
        {
            playList.Id = null;
            var palyListContext = mapper.Map<PlayListContext>(playList);

            this.user.PlayLists.Add(palyListContext);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = mapper.Map<PlayList>(palyListContext);

            return result;
        }

        public PlayList GetPlayList(int id)
        {
            var playListContext = this.user.PlayLists.FirstOrDefault(x => x.Id == id);

            var playList = mapper.Map<PlayList>(playListContext);

            return playList;
        }

        public List<PlayList> GetPlayLists()
        {
            var playListsContext = this.user.PlayLists.ToList();

            var playLists = mapper.Map<List<PlayList>>(playListsContext);
            return playLists;
        }

        public async  Task<string> Remove(int id)
        {
            var tmp = new PlayListContext
            {
                Id = id
            };

            if (!this.user.PlayLists.Contains(tmp))
            {
                return "شناسه مورد نظر یافت نشد.";
            }

            this.user.PlayLists.Remove(tmp);

            await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

            return "عملیات با موفقیت انجام شد.";
        }

        public async Task<string> RemoveSong(PlayListSongInformationRequest songInformationRequest)
        {
            var playListContext = this.user.PlayLists.FirstOrDefault
                (x => x.Id == songInformationRequest.PlayListId);

            if (playListContext == null)
            {
                return "شناسه مورد نظر یافت نشد.";
            }

            var tmp = new SongContext
            {
                Id = songInformationRequest.SongId
            };

            if (!playListContext.Songs.Contains(tmp))
            {
                return "شناسه مورد نظر یافت نشد.";
            }

            var song = playListContext.Songs.Remove(tmp);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

            return "عملیات با موفقیت انجام شد.";
        }
    }
}
