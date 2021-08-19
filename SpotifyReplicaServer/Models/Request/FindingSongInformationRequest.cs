namespace SpotifyReplicaServer.Models.Request
{
    public class FindingSongInformationRequest
    {
        public string Phrase { get; set; }
        public int count { get; set; }
        public string Sorter { get; set; }
        public bool Desc { get; set; }
    }
}
