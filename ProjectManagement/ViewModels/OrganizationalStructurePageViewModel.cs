using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ProjectManagement.ViewModels
{
    internal class OrganizationalStructurePageViewModel: ViewModel
    {
        private readonly bool _adminCurrent = CurrentEmployee.currentEmployee.Admin;
        public Visibility AdminCurrent
        {
            get => _adminCurrent ? Visibility.Visible : Visibility.Collapsed;
        }

        private List<Employee> _employees = new ();
        public List<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }

        private String _filterStr = "";
        public String FilterStr
        {
            get => _filterStr;
            set { Set(ref _filterStr, value);
                using (ProjectManagementContext db = new())
                {
                    db.Posts.Load();
                    db.Departments.Load();
                    Employees = db.Employees.Where(e => e.Blocked == false && (e.Name.StartsWith(FilterStr) ||
                                                                               e.Surname.StartsWith(FilterStr) ||
                                                                               e.Patronymic.StartsWith(FilterStr) ||
                                                                               e.PostNavigation.DepartmentNavigation.Name.StartsWith(FilterStr))).ToList();
                    SelectEmployee = Employees.FirstOrDefault();
                }
            }
        }

        private Employee? _selectEmployee;
        public Employee? SelectEmployee
        {
            get => _selectEmployee;
            set => Set(ref _selectEmployee, value);
        }

        private ObservableCollection<Department> _departments = new ();
        public ObservableCollection<Department> Departments
        {
            get => _departments;
            set => Set(ref _departments, value);
        }

        private Department? _selectDepartament;
        public Department? SelectDepartament
        {
            get => _selectDepartament;
            set {
                Set(ref _selectDepartament, value);
                SelectPost = SelectDepartament.Posts.FirstOrDefault();
            }
        }

        private Post? _selectPost;
        public Post? SelectPost
        {
            get => _selectPost;
            set
            {
                Set(ref _selectPost, value);
            }
        }

        public OrganizationalStructurePageViewModel()
        {
            using(ProjectManagementContext db = new())
            {
                db.Posts.Load();
                db.Departments.Load();
                db.Posts.Load();
                Employees = db.Employees.Where(e => e.Blocked == false && (e.Name.StartsWith(FilterStr) ||
                                                                           e.Surname.StartsWith(FilterStr) ||
                                                                           e.Patronymic.StartsWith(FilterStr) ||
                                                                           e.PostNavigation.DepartmentNavigation.Name.StartsWith(FilterStr))).ToList();
                SelectEmployee = Employees.FirstOrDefault();

                Departments = db.Departments.Local.ToObservableCollection();
                SelectDepartament = Departments.FirstOrDefault();

            }
        }
    }
}
