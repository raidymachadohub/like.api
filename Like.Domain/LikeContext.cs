using Like.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Like.Domain
{
    public class LikeContext : DbContext
    {
        public DbSet<Blog> users { get; set; }
        public LikeContext(DbContextOptions<LikeContext> options) : base(options) { }
    }
}
