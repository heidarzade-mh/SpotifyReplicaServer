using System.Collections.Generic;

namespace SpotifyReplicaServer.Models
{
    public class PlayList
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<Song> songs { get; set; }
    }
}
