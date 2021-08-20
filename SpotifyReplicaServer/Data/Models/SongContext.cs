using System;
using System.Collections.Generic;

namespace SpotifyReplicaServer.Data.Models
{
    public class SongContext
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Lyrics { get; set; }
        public string File { get; set; }
        public string Cover { get; set; }
        public string PublishDate { get; set; }

        public List<PlayListContext> PlayLists { get; set; }

        public SongContext()
        {
            this.PlayLists = new List<PlayListContext>();
        }

        public override bool Equals(object obj)
        {
            return obj is SongContext context &&
                   Id == context.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
