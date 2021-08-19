namespace SpotifyReplicaServer.Models.Request
{
    public class PagingInformationRequest
    {
        public int Size { get; set; }
        public int Current { get; set; }
        public string Sorter { get; set; }
        public bool Desc { get; set; }
    }
}
