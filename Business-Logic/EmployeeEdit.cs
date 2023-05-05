using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using Csla.Security;

namespace Business_Logic
{
    [Serializable]
    public class EmployeeEdit  : BusinessBase<EmployeeEdit>
    {


        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
        public static readonly PropertyInfo<DateTime> HireDateProperty = RegisterProperty<DateTime>(nameof(HireDate));
        public static readonly PropertyInfo<decimal> SalaryProperty = RegisterProperty<decimal>(nameof(Salary));

        public int Id
        {
            get => GetProperty(IdProperty); 
            set  => SetProperty(IdProperty, value); 
        }
        public string Name
        {
            get => GetProperty(NameProperty);
            set => SetProperty(NameProperty, value);
        }

        public DateTime HireDate
        {
            get => GetProperty(HireDateProperty);
            set => SetProperty(HireDateProperty, value);
        }

        public decimal Salary
        {
            get => GetProperty(SalaryProperty);
            set => SetProperty(SalaryProperty, value);
        }
        
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new CheckCaseRule(NameProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.ReadProperty, SalaryProperty, "admin"));
        }



        [Fetch]
        private async void Fetch(int id, [Inject] Data_Layer.Interface.IEmployeService dataAccesslayer)
        {
            var data = await dataAccesslayer.GetEmployee(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Name = data.Name;
                HireDate = data.HireDate;
                Salary = data.Salary;
            };
        }

        [Insert]
        private void Insert([Inject] Data_Layer.Interface.IEmployeService dataAccessLayer)
        {
            using (BypassPropertyChecks)
            {
                var employe = new Data_Layer.Model.Employee()
                {
                    Id = 9,
                    Name = Name,
                    HireDate = DateTime.Now,
                    Salary = 121212
                };
                dataAccessLayer.AddEmployee(employe);
            }
        }

      
        [Delete]
        private async Task DeleteAsync(int id, [Inject] Data_Layer.Interface.IEmployeService dataAccessLayer)
        {

            bool success = false;
            if (!CanTheCurrentRoleDelete())
            {
              await    dataAccessLayer.Delete(id);
            }
            else
            {
                throw new Exception("You dont have any persmission to delete");
            }
           
        }


        private bool CanTheCurrentRoleDelete()
        {
           
            return ApplicationContext.User.IsInRole("admin");
        }

        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            BusinessRules.AddRule(typeof(EmployeeEdit),
              new Csla.Rules.CommonRules.IsInRole(AuthorizationActions.DeleteObject, "admin"));
        }   


    }
}