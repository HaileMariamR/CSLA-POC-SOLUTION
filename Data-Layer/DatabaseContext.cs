using Data_Layer.Configuration;
using Data_Layer.Model;
using Microsoft.EntityFrameworkCore;


namespace Data_Layer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)  : base(options){}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
