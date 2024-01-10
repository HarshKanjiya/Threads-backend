
using AuthAPI.microservice.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.microservice.data
{
    public class DBcontextUser : IdentityDbContext<UserModel>
    {
        public DBcontextUser(DbContextOptions<DBcontextUser> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
