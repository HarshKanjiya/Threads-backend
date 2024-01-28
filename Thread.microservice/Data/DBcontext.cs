using Microsoft.EntityFrameworkCore;
using Thread.microservice.Model;
using Thread.Model;

namespace Thread.Data
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }

        public DbSet<ThreadModel> Threads { get; set; }

        public DbSet<ThreadContent> Contents { get; set; }

        public DbSet<PollResponseModel> PollResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
