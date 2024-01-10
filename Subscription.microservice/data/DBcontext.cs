using Microsoft.EntityFrameworkCore;
using Subscription.microservice.Models;

namespace Subscription.microservice.data
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }

        public DbSet<SubscriptionModel> Subscriptions { get; set; }

        public DbSet<ReportCategory> ReportCategories { get; set; }
        public DbSet<ReportMessages> ReportMessages { get; set; }
        public DbSet<ReportModel> Reports { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
