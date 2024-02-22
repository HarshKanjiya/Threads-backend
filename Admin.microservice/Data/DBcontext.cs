using Admin.microservice.Model;
using Microsoft.EntityFrameworkCore;
namespace Admin.microservice.Data
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }


        public DbSet<ReportCategoryModel> ReportCategories { get; set; }
        public DbSet<ReportModel> AvailableReports { get; set; }
        public DbSet<UserReportModel> UserReports { get; set; }
        public DbSet<CustomReportModel> CustomReports { get; set; }
        public DbSet<BugReportModel> BugReports { get; set; }
        public DbSet<FilesModel> BugProofs { get; set; }


        public DbSet<EnvVarModel> EnvironmentVariables { get; set; }
    }
}


