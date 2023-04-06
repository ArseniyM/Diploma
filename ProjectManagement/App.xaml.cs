using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Services;
using ProjectManagement.ViewModels;
using ProjectManagement.Views.Windows;
using System;
using System.Windows;

namespace ProjectManagement
{
    public partial class App : Application
    {
        private static IServiceProvider? _Services;
        public static IServiceProvider Services => _Services ??= InitializeServices().BuildServiceProvider();

        private static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<AuthorizationWindowViewModel>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<IUserDialog, UserDialogServices>();

            services.AddTransient(s =>
            {
                var model = s.GetRequiredService<AuthorizationWindowViewModel>();
                var window = new AuthorizationWindow() { DataContext = model };
                model.DialogComplete += (_, _) => window.Close();

                return window;
            });

            services.AddTransient(s =>
            {
                var model = s.GetRequiredService<MainWindowViewModel>();
                var window = new MainWindow() { DataContext = model };
                model.DialogComplete += (_, _) => window.Close();

                return window;
            });

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Services.GetRequiredService<IUserDialog>().OpenAuthorizationWindow();
        }
    }
}
