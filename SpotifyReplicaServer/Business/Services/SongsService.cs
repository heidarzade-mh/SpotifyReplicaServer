using AutoMapper;
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
    public class SongsService : ISongsService
    {
        private readonly DatabaseContext dbContext;
        private readonly IMapper mapper;

        public SongsService(DatabaseContext dbContext, IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<Song> GetSongs()
        {
            var songs = dbContext.Songs.ToList();
            songs.Sort((x, y) => x.Name.CompareTo(y.Name));
            return mapper.Map<List<Song>>(songs);
        }

        public Song GetSong(int id)
        {
            var song = dbContext.Songs.FirstOrDefault(x => x.Id == id);

            return mapper.Map<Song>(song);
        }

        public List<Song> GetPagedSongs(PagingInformationRequest pagingInformation)
        {
            var songsContext = dbContext.Songs.ToList();
            var songs = mapper.Map<List<Song>>(songsContext);

            SortSongs(songs, pagingInformation.Sorter, pagingInformation.Desc);

            var start = (pagingInformation.Current - 1) * pagingInformation.Size;
            var size = pagingInformation.Size;
            try
            {
                if (start + size - 1 > songs.Count)
                {
                    return songs.GetRange(start, songs.Count - start);
                }

                var result = songs.GetRange(start, pagingInformation.Size);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Song> FindSongs(FindingSongInformationRequest findingSongInformation)
        {
            var songsContext = dbContext.Songs.ToList();
            var songs = mapper.Map<List<Song>>(songsContext);

            var filteredSongs = songs.Where(
                x =>
                x.Name.Contains(findingSongInformation.Phrase) ||
                x.Artist.Contains(findingSongInformation.Phrase) ||
                x.Lyrics.Contains(findingSongInformation.Phrase)
                ).ToList();

            SortSongs(filteredSongs, findingSongInformation.Sorter, findingSongInformation.Desc);

            if (findingSongInformation.count != 0 && findingSongInformation.count < filteredSongs.Count)
            {
                filteredSongs = filteredSongs.GetRange(0, findingSongInformation.count);
            }

            return filteredSongs;
        }

        private void SortSongs(List<Song> songs, string sorter, bool desc)
        {
            songs.Sort((x, y) => x.Name.CompareTo(y.Name));
            if (sorter == "artist")
            {
                songs.Sort((x, y) => x.Artist.CompareTo(y.Artist));
            }

            if (desc)
            {
                songs.Reverse();
            }
        }

        public async Task<string> AddSongs(List<Song> songs)
        {
            var songsContext = this.mapper.Map<List<SongContext>>(songs);

            this.dbContext.Songs.AddRange(songsContext);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

            return "salam";
        }
    }
}
