using Microsoft.EntityFrameworkCore;
using UserActions.microservice.Models;
namespace UserActions.microservice.Data
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }

        public DbSet<LikeModel> Likes { get; set; }
        public DbSet<PollResponseModel> PollResponses { get; set; }
        public DbSet<RelationshipModel> Relationships { get; set; }
    }
}


