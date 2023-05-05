using Data_Layer.Interface;
using Data_Layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Services
{
    public class EmployeService : IEmployeService
    {

        private readonly DatabaseContext _dbContext;

        public EmployeService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChangesAsync();
            return  Task.CompletedTask;
        }

        public async Task<bool> Delete(int id)
        {
            var employee = await _dbContext.Employees.Where(e => e.Id == id ).FirstOrDefaultAsync();
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<List<Employee>> GetAllEmployes()
        {
            return _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee>  GetEmployee(int employeeId)
        {
            var employee =  await _dbContext.Employees.Where(p => p.Id == employeeId).FirstOrDefaultAsync();
            if (employee != null)
                return employee;
            else
                throw new KeyNotFoundException($"Id {employeeId}");
        }
    }
}
