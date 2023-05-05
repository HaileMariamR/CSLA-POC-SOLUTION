using Data_Layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Interface
{
    public interface IEmployeService
    {

      Task<Employee> GetEmployee(int employeeId);
      Task<List<Employee>> GetAllEmployes();
      Task AddEmployee(Employee employee);
      Task<bool> Delete(int id);


    }
}
