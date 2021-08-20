using System.Collections.Generic;

namespace SpotifyReplicaServer.Data.Models
{
    public class PlayListContext
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<SongContext> Songs { get; set; }
    }
}
