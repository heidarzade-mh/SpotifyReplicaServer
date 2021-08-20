using Microsoft.EntityFrameworkCore;
using SpotifyReplicaServer.Data.Models;

namespace SpotifyReplicaServer.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<UserContext> Users { get; set; }
        public DbSet<SongContext> Songs { get; set; }
        public DbSet<PlayListContext> PlayLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserContext>()
                .HasMany(u => u.PlayLists)
                .WithOne();

            modelBuilder.Entity<SongContext>()
                .HasMany(s => s.PlayLists)
                .WithMany(pl => pl.Songs);
        }
    }
}