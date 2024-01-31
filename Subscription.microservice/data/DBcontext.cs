using Microsoft.EntityFrameworkCore;
using Subscription.microservice.Models;

namespace Subscription.microservice.data
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }

        public DbSet<SubscriptionModel> Subscriptions { get; set; }
        public DbSet<PackagesModel> Packages { get; set; }


    }
}
