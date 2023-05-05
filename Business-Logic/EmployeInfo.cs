using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
namespace Business_Logic
{
    [Serializable]
    public class EmployeInfo : ReadOnlyBase<EmployeInfo>
    {

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
        public static readonly PropertyInfo<DateTime> HireDateProperty = RegisterProperty<DateTime>(nameof(HireDate));
        public static readonly PropertyInfo<decimal> SalaryProperty = RegisterProperty<decimal>(nameof(Salary));

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }
        public string Name
        {
            get => GetProperty(NameProperty);
            private   set => LoadProperty(NameProperty, value);
        }

        public DateTime HireDate
        {
            get => GetProperty(HireDateProperty);
            private  set => LoadProperty(HireDateProperty, value);
        }

        public decimal Salary
        {
            get => GetProperty(SalaryProperty);
            private  set => LoadProperty(SalaryProperty, value);
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new CheckCaseRule(NameProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.ReadProperty, SalaryProperty, "admin"));
        }

        [FetchChild]
        private void Fetch(Data_Layer.Model.Employee data)
        {
            Id = data.Id;
            Name = data.Name;
            HireDate = data.HireDate;
            Salary = data.Salary;
        }
    }
}
