using System.ComponentModel.DataAnnotations;

namespace SpotifyReplicaServer.Models.Transfer
{
    public class UserViewModel
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }

        public string Avatar { get; set; }
        public Gender Gender { get; set; }
        public string BirthDate { get; set; }
    }
}
