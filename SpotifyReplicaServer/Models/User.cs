using System;
using System.Text.Json.Serialization;

namespace SpotifyReplicaServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
        public string BirthDate { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   (Username == user.Username ||
                   Email == user.Email);
        }
    }
}
