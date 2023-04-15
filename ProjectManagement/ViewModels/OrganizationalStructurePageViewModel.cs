using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.ViewModels
{
    internal class OrganizationalStructurePageViewModel: ViewModel
    {
        private List<Employee> _employees = new ();
        public List<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }

        public OrganizationalStructurePageViewModel()
        {
            using(ProjectManagementContext db = new())
            {
                Employees = db.Employees.Where(e => e.Blocked == true).ToList();
            }
        }
    }
}
