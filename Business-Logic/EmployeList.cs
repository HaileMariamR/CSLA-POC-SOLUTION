using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.DataPortalClient;
using Data_Layer.Model;

namespace Business_Logic
{
    [Serializable]
    public class EmployeList : ReadOnlyListBase<EmployeList, EmployeInfo>
    {
      
        [Fetch]
        private async Task FetchAsync([Inject] Data_Layer.Interface.IEmployeService dal, [Inject] IChildDataPortal<EmployeInfo> employeInfoPortal)
        {
            using (LoadListMode)
            {


                Task<List<Employee>> employeeListTask = dal.GetAllEmployes();
                List<Employee> employeeList = await employeeListTask;
                var data = employeeList.Select(d => employeInfoPortal.FetchChild(d));
                AddRange(data);
            }
        }

    }
}
