using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Views.Windows;
using System;

namespace ProjectManagement.Services
{
    internal class UserDialogServices: IUserDialog
    {
        private readonly IServiceProvider _Services;

        public UserDialogServices(IServiceProvider Services) => _Services = Services;

        private MainWindow? _MainWindow;
        public void OpenMainWindow()
        {
            if (_MainWindow is { } window)
            {
                window.Show();
                return;
            }

            window = _Services.GetRequiredService<MainWindow>();
            window.Closed += (_, _) => _MainWindow = null;

            _MainWindow = window;
            window.Show();
        }

        private AuthorizationWindow? _AuthorizationWindow;
        public void OpenAuthorizationWindow()
        {
            if (_AuthorizationWindow is { } window)
            {
                window.Show();
                return;
            }

            window = _Services.GetRequiredService<AuthorizationWindow>();
            window.Closed += (_, _) => _AuthorizationWindow = null;

            _AuthorizationWindow = window;
            window.Show();
        }

        private AddDepartmentWindow? _AddDepartmentWindow;
        public void OpenAddDepartmentWindow()
        {
            if (_AddDepartmentWindow is { } window)
            {
                window.ShowDialog();
                return;
            }

            window = _Services.GetRequiredService<AddDepartmentWindow>();
            window.Closed += (_, _) => _AddDepartmentWindow = null;

            _AddDepartmentWindow = window;
            window.ShowDialog();
        }

        private EditDepartmentWindow? _EditDepartmentWindow;
        public void OpenEditDepartmentWindow()
        {
            if (_EditDepartmentWindow is { } window)
            {
                window.ShowDialog();
                return;
            }

            window = _Services.GetRequiredService<EditDepartmentWindow>();
            window.Closed += (_, _) => _EditDepartmentWindow = null;

            _EditDepartmentWindow = window;
            window.ShowDialog();
        }

        private AddPostWindow? _AddPostWindow;
        public void OpenAddPostWindow()
        {
            if (_AddPostWindow is { } window)
            {
                window.ShowDialog();
                return;
            }

            window = _Services.GetRequiredService<AddPostWindow>();
            window.Closed += (_, _) => _AddPostWindow = null;

            _AddPostWindow = window;
            window.ShowDialog();
        }

        private EditPostWindow? _EditPostWindow;
        public void OpenEditPostWindow()
        {
            if (_EditPostWindow is { } window)
            {
                window.ShowDialog();
                return;
            }

            window = _Services.GetRequiredService<EditPostWindow>();
            window.Closed += (_, _) => _EditPostWindow = null;

            _EditPostWindow = window;
            window.ShowDialog();
        }

        private AddEmployeeWindow? _AddEmployeeWindow;
        public void OpenAddEmployeeWindow()
        {
            if (_AddEmployeeWindow is { } window)
            {
                window.ShowDialog();
                return;
            }

            window = _Services.GetRequiredService<AddEmployeeWindow>();
            window.Closed += (_, _) => _AddEmployeeWindow = null;

            _AddEmployeeWindow = window;
            window.ShowDialog();
        }

        private EditEmployeeWindow? _EditEmployeeWindow;
        public void OpenEditEmployeeWindow()
        {
            if (_EditEmployeeWindow is { } window)
            {
                window.ShowDialog();
                return;
            }

            window = _Services.GetRequiredService<EditEmployeeWindow>();
            window.Closed += (_, _) => _EditEmployeeWindow = null;

            _EditEmployeeWindow = window;
            window.ShowDialog();
        }
    }
}
