using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreApplication.Models
{
    public class EmployeeeContext:DbContext
    {
        public EmployeeeContext(DbContextOptions<EmployeeeContext>options): base(options)
        {

            
        }
        public DbSet<Employeee> Employees { get; set; }

    }
}
