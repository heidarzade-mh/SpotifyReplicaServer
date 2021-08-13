using Microsoft.EntityFrameworkCore;
using SpotifyReplicaServer.Models;

namespace TodoApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<User> TodoItems { get; set; }
    }
}