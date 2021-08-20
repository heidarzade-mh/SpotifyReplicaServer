using System;
using System.Collections.Generic;

namespace SpotifyReplicaServer.Data.Models
{
    public class PlayListContext
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<SongContext> Songs { get; set; }

        public PlayListContext()
        {
            this.Songs = new List<SongContext>();
        }

        public override bool Equals(object obj)
        {
            return obj is PlayListContext context &&
                   Id == context.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
