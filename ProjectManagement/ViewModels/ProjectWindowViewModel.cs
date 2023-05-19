using GongSolutions.Wpf.DragDrop;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Infrastructure.Commands;
using ProjectManagement.Models;
using ProjectManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectManagement.ViewModels
{
    internal class ProjectWindowViewModel: ViewModel, IDropTarget
    {
        #region Реализация IDropTarget для DragAndDrop задач на канбан
        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo != null && dropInfo.Data is Task task && dropInfo.VisualTarget is ListBox list)
            {
                if (((Phase)list.DataContext).Id == task.Phase && task.Status == 1 && (list.Name == "ToDo" || list.Name == "Done") && (task.Executor == CurrentEmployee.currentEmployee.Id) ||
                    ((Phase)list.DataContext).Id == task.Phase && task.Status == 2 && (list.Name == "Done") && (task.Executor == CurrentEmployee.currentEmployee.Id))
                {
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    dropInfo.Effects = DragDropEffects.Move;

                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo != null && dropInfo.Data is Task task && dropInfo.VisualTarget is ListBox list)
            {
                using(ProjectManagementContext db = new ())
                {
                    db.Tasks.Load();
                    db.Employees.Load();
                    if (list.Name == "ToDo")
                    {
                        db.Tasks.Single(e => e.Id == task.Id).Status = 2;
                    }
                    else if (list.Name == "Done")
                    {
                        db.Tasks.Single(e => e.Id == task.Id).Status = 3;
                    }
                    db.SaveChanges();
                    Phases = db.Projects.Include(e => e.Phases).Single(e => e.Id == Project.Id).Phases.ToList();
                }
            }
        }
        #endregion

        private readonly Visibility _visibilityAdmin;
        public Visibility VisibilityAdmin
        {
            get => _visibilityAdmin;
        }

        private Project _project;
        public Project Project
        {
            get => _project;
            set => Set(ref  _project, value);
        }

        private String _nameProject;
        public String NameProject
        {
            get => _nameProject;
            set => Set(ref _nameProject, value);
        }

        private ObservableCollection<Employee> _employeesProject = new ();
        public ObservableCollection<Employee> EmployeesProject
        {
            get => _employeesProject;
            set => Set(ref _employeesProject, value);
        }

        private List<Phase> _phases = new ();
        public List<Phase> Phases
        {
            get => _phases;
            set => Set(ref _phases, value);
        }


        #region Команды

        #region AddEmployeesProjectCommand - команда удаления сотрудника из проекта
        public ICommand AddEmployeesProjectCommand { get; }
        private void OnAddEmployeesProjectCommandExecuted(object p)
        {
            ChangingListEmployees.Employees = EmployeesProject.ToList();
            App.Services.GetRequiredService<IUserDialog>().OpenEditEmployeesProjectWindow();
            ChangingListEmployees.Employees = null!;
            if (ChangingEmployee.changingEmployee != null)
            {
                using (ProjectManagementContext db = new ())
                {
                    db.Employees.Include(e => e.Projects).Single(e => e.Id == ChangingEmployee.changingEmployee.Id).Projects.Add(db.Projects.Single(e => e.Id == Project.Id));
                    db.SaveChanges();
                    EmployeesProject.Clear();
                    db.Employees.Where(e => e.Projects.Contains(Project)).ToList().ForEach(EmployeesProject.Add);
                }
                ChangingEmployee.changingEmployee = null!;
            }
        }
        private bool CanAddEmployeesProjectCommandExecute(object p) => true;
        #endregion

        #region AddPhaseProjectCommand - добавление фазы
        public ICommand AddPhaseProjectCommand { get; }
        private void OnAddPhaseProjectCommandExecuted(object p)
        {
            App.Services.GetRequiredService<IUserDialog>().OpenAddPhaseWindow();
            using (ProjectManagementContext db = new())
            {
                db.Tasks.Load();
                db.Employees.Load();
                Phases = db.Projects.Include(e => e.Phases).Single(e => e.Id == Project.Id).Phases.ToList();
            }
        }
        private bool CanAddPhaseProjectCommandExecute(object p) => true;
        #endregion

        #region AddTaskProjectCommand - добавление задачи
        public ICommand AddTaskProjectCommand { get; }
        private void OnAddTaskProjectCommandExecuted(object p)
        {
            App.Services.GetRequiredService<IUserDialog>().OpenAddTaskWindow();
            using (ProjectManagementContext db = new())
            {
                db.Tasks.Load();
                db.Employees.Load();
                Phases = db.Projects.Include(e => e.Phases).Single(e => e.Id == Project.Id).Phases.ToList();
            }
        }
        private bool CanAddTaskProjectCommandExecute(object p) => true;
        #endregion

        #region EditTaskProjectCommand - изменение задачи
        public ICommand EditTaskProjectCommand { get; }
        private void OnEditTaskProjectCommandExecuted(object p)
        {
            ChangingTask.task = p as Task; 
            App.Services.GetRequiredService<IUserDialog>().OpenEditTaskWindow();
            ChangingTask.task = null;
            using (ProjectManagementContext db = new())
            {
                db.Tasks.Load();
                db.Employees.Load();
                Phases = db.Projects.Include(e => e.Phases).Single(e => e.Id == Project.Id).Phases.ToList();
            }
        }
        private bool CanEditTaskProjectCommandExecute(object p) => p is Task;
        #endregion

        #region DeleteSelectEmployeesCommand - команда удаления сотрудника из проекта
        public ICommand DeleteSelectEmployeesCommand { get; }
        private void OnDeleteSelectEmployeesCommandExecuted(object p)
        {
            if (p is Employee emp)
            {
                try
                {
                    if (MessageBox.Show($"Вы уверены, что хотите удалить {emp.Surname} {emp.Name} {emp.Patronymic} из проекта \"{Project.Name}\"", "Подтверждение",
                        MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        using (ProjectManagementContext db = new())
                        {
                            db.Tasks.Load();
                            db.Phases.Load();
                            db.Projects.Load();
                            db.Employees.Load();
                            Project pr = db.Projects.Include(e => e.Phases).Where(e => e.Id == Project.Id).First();

                            if (!pr.Phases.Where(e => e.Tasks.Where(i => i.Executor == emp.Id).Any()).Any())
                            {
                                
                                db.Employees.Where(e => e.Id == emp.Id).Include(e => e.Projects).Single().Projects.Remove(pr);
                                db.SaveChanges();
                                EmployeesProject.Clear();
                                db.Employees.Where(e => e.Projects.Contains(pr)).ToList().ForEach(EmployeesProject.Add);
                            }
                            else
                            {
                                MessageBox.Show("Нельзя удалить из проекта сотрудника, для которого назначены задачи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private bool CanDeleteSelectEmployeesCommandExecute(object p) => true;
        #endregion
        #endregion

        public ProjectWindowViewModel()
        {
            AddPhaseProjectCommand = new RelayCommand(OnAddPhaseProjectCommandExecuted, CanAddPhaseProjectCommandExecute);
            AddTaskProjectCommand = new RelayCommand(OnAddTaskProjectCommandExecuted, CanAddTaskProjectCommandExecute);
            EditTaskProjectCommand = new RelayCommand(OnEditTaskProjectCommandExecuted, CanEditTaskProjectCommandExecute);
            AddEmployeesProjectCommand = new RelayCommand(OnAddEmployeesProjectCommandExecuted, CanAddEmployeesProjectCommandExecute);
            DeleteSelectEmployeesCommand = new RelayCommand(OnDeleteSelectEmployeesCommandExecuted, CanDeleteSelectEmployeesCommandExecute);

            _visibilityAdmin = CurrentEmployee.currentEmployee.Admin ? Visibility.Visible : Visibility.Collapsed;
            Project = ChangingProject.project;
            NameProject = Project.Name;
            using (ProjectManagementContext db = new ProjectManagementContext())
            {
                db.Projects.Load();
                db.Tasks.Load();
                db.Employees.Where(e => e.Projects.Contains(Project)).ToList().ForEach(EmployeesProject.Add);
                Phases = db.Projects.Include(e => e.Phases).Single(e => e.Id == Project.Id).Phases.ToList();
            }
        }
    }
}
