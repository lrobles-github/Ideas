using Microsoft.EntityFrameworkCore;
 
namespace Ideas.Models
{
    public class IdeasContext : DbContext
    {
        public IdeasContext(DbContextOptions<IdeasContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Like> Likes { get; set; }
    
    }
}