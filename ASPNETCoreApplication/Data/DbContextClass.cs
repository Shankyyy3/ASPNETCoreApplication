using ASPNETCoreApplication.Entities;
using Microsoft.EntityFrameworkCore;
namespace ASPNETCoreApplication.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("EmployeeAppCon"));
        }

        public DbSet<FileDetails> FileDetails { get; set; }
    }
}

