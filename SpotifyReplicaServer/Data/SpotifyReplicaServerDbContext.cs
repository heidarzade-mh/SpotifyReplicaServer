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

        public DbSet<User> TodoItems { get; set; }
    }
}