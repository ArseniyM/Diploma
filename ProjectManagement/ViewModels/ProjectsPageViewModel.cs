using ProjectManagement.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagement.ViewModels
{
    class ProjectsPageViewModel: ViewModel
    {
        private List<Project> _projects = new();
        public List<Project> Projects
        {
            get => _projects;
            set => Set(ref _projects, value);
        }

        private Visibility _visibilityFilter = Visibility.Collapsed;
        public Visibility VisibilityFilter
        {
            get => _visibilityFilter;
            set => Set(ref _visibilityFilter, value);
        }

        private String? _startDateFirst;
        public String? StartDateFirst
        {
            get => _startDateFirst;
            set {
                Set(ref _startDateFirst, value);
                Filter();
            }
        }

        private String? _startDateSecond;
        public String? StartDateSecond
        {
            get => _startDateSecond;
            set
            {
                Set(ref _startDateSecond, value);
                Filter();
            }
        }

        private String? _completDateFirst;
        public String? CompletDateFirst
        {
            get => _completDateFirst;
            set { 
                Set(ref _completDateFirst, value);
                Filter();
            }
        }

        private String? _completDateSecond;
        public String? CompletDateSecond
        {
            get => _completDateSecond;
            set
            {
                Set(ref _completDateSecond, value);
                Filter();
            }
        }

        private bool _allStatus = true;
        public bool AllStatus
        {
            get => _allStatus;
            set
            {
                Set(ref _allStatus, value);
                Filter();
            }
        }

        private bool _completStatus = false;
        public bool CompletStatus
        {
            get => _completStatus;
            set
            {
                Set(ref _completStatus, value);
                Filter();
            }
        }

        private bool _noCompletStatus = false;
        public bool NoCompletStatus
        {
            get => _noCompletStatus;
            set
            {
                Set(ref _noCompletStatus, value);
                Filter();
            }
        }

        private String _filterStr = "";
        public String FilterStr
        {
            get => _filterStr;
            set
            {
                Set(ref _filterStr, value);
                Filter();
            }
        }

        public ICommand VisibilityFilterCommand { get; }
        private void OnVisibilityFilterCommandExecuted(object p) {
            if (VisibilityFilter == Visibility.Collapsed) VisibilityFilter = Visibility.Visible;
            else VisibilityFilter = Visibility.Collapsed;
        }
        private bool CanVisibilityFilterCommandExecute(object p) => true;

        private void Filter()
        {

            using (ProjectManagementContext db = new ())
            {
                Projects = db.Projects.Where(e => e.Name.Contains(FilterStr)).ToList();
            }

            DateOnly date;

            if (Projects.Count != 0 && DateOnly.TryParse(StartDateFirst, out date))
                Projects = Projects.Where(e => e.StartDate > date).ToList();

            if (Projects.Count != 0 && DateOnly.TryParse(StartDateSecond, out date))
                Projects = Projects.Where(e => e.StartDate < date).ToList();

            if (Projects.Count != 0 && DateOnly.TryParse(CompletDateFirst, out date))
                Projects = Projects.Where(e => e.CompletDate < date).ToList();

            if (Projects.Count != 0 && DateOnly.TryParse(CompletDateSecond, out date))
                Projects = Projects.Where(e => e.CompletDate > date).ToList();

            if (Projects.Count != 0 && CompletStatus)
                Projects = Projects.Where(e => e.Completed).ToList();
            else if (Projects.Count != 0 && NoCompletStatus)
                Projects = Projects.Where(e => !e.Completed).ToList();

        }

        public ProjectsPageViewModel()
        {
            VisibilityFilterCommand = new RelayCommand(OnVisibilityFilterCommandExecuted, CanVisibilityFilterCommandExecute);
            using (ProjectManagementContext db = new())
            {
                Projects = db.Projects.ToList();
            }
        }
    }
}
