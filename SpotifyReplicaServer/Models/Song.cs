﻿namespace SpotifyReplicaServer.Models
{
    public class Song
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Lyrics { get; set; }
        public string File { get; set; }
        public string Cover { get; set; }
        public string PublishDate { get; set; }
    }
}
