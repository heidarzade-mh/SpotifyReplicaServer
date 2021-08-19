using SpotifyReplicaServer.Abstraction;
using SpotifyReplicaServer.Data;
using SpotifyReplicaServer.Models;
using SpotifyReplicaServer.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyReplicaServer.Shared.Services
{
    public class SongsService : ISongsService
    {
        private readonly SpotifyReplicaServerDbContext dbContext;
        public SongsService(SpotifyReplicaServerDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<Song> GetSongs()
        {
            var songs = dbContext.Songs.ToList();
            songs.Sort((x, y) => x.Name.CompareTo(y.Name));
            return songs;
        }

        public Song GetSong(int id)
        {
            var song = dbContext.Songs.FirstOrDefault<Song>(x => x.Id == id);
            return song;
        }

        public List<Song> GetPagedSongs(PagingInformationRequest pagingInformation)
        {
            var songs = dbContext.Songs.ToList();
            SortSongs(songs, pagingInformation.Sorter, pagingInformation.Desc);

            var start = (pagingInformation.Current - 1) * pagingInformation.Size;
            var result = songs.GetRange(start, pagingInformation.Size);

            return result;
        }

        public List<Song> FindSongs(FindingSongInformationRequest findingSongInformation)
        {
            var songs = dbContext.Songs.ToList();

            var filteredSongs = songs.Where(
                x =>
                x.Name.Contains(findingSongInformation.Phrase) ||
                x.Artist.Contains(findingSongInformation.Phrase) ||
                x.Lyrics.Contains(findingSongInformation.Phrase)
                ).ToList();

            SortSongs(filteredSongs, findingSongInformation.Sorter, findingSongInformation.Desc);

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
    }
}
