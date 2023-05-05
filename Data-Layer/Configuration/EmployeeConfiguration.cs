using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Layer.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            SetSeedData(builder);
        }

        private void SetSeedData(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(new Employee { Id = 1, Name = "Hailemariam", HireDate = DateTime.Now, Salary = 234234234 });
            builder.HasData(new Employee { Id = 2, Name = "Fikadie", HireDate = DateTime.Now, Salary = 234234234 });
            builder.HasData(new Employee { Id = 3, Name = "Harry", HireDate = DateTime.Now, Salary = 234234234 });

        }
    }
}
