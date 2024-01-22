using Microsoft.EntityFrameworkCore;
using UserApi.microservice.Models;

namespace UserApi.microservice.Data
{
    public class DbContextUsers : DbContext
    {

        public DbContextUsers(DbContextOptions<DbContextUsers> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<OtpModel> Otps { get; set; }
        public DbSet<PasswordTokenModel> PasswordTokens { get; set; }
    }
}
