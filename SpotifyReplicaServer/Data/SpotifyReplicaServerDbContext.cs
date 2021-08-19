using Microsoft.EntityFrameworkCore;
using SpotifyReplicaServer.Models;

namespace SpotifyReplicaServer.Data
{
    public class SpotifyReplicaServerDbContext : DbContext
    {
        public SpotifyReplicaServerDbContext(DbContextOptions<SpotifyReplicaServerDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}