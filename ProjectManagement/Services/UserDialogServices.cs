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
    }
}
