using BlogAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions options) : base(options)
        {       
        }
        // Dbset
        public DbSet<Post> Posts { get; set; }
    }
}
