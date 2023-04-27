using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Services;
using ProjectManagement.Services.NavigatorPages;
using ProjectManagement.ViewModels;
using ProjectManagement.ViewModels.Base;
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
            services.AddTransient<AddDepartmentWindowViewModel>();
            services.AddTransient<EditDepartmentWindowViewModel>();
            services.AddTransient<AddPostWindowViewModel>();
            services.AddTransient<EditPostWindowViewModel>();
            services.AddTransient<AddEmployeeWindowViewModel>();
            services.AddTransient<EditEmployeeWindowViewModel>();

            services.AddSingleton<IUserDialog, UserDialogServices>();
            services.AddSingleton<IPageResolver, PagesResolver>();
            services.AddSingleton<IViewModelsResolver, ViewModelsResolver>();

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

            services.AddTransient(s =>
            {
                var model = s.GetRequiredService<AddDepartmentWindowViewModel>();
                var window = new AddDepartmentWindow() { DataContext = model };
                model.DialogComplete += (_, _) => window.Close();

                return window;
            });

            services.AddTransient(s =>
            {
                var model = s.GetRequiredService<EditDepartmentWindowViewModel>();
                var window = new EditDepartmentWindow() { DataContext = model };
                model.DialogComplete += (_, _) => window.Close();

                return window;
            });

            services.AddTransient(s =>
            {
                var model = s.GetRequiredService<AddPostWindowViewModel>();
                var window = new AddPostWindow() { DataContext = model };
                model.DialogComplete += (_, _) => window.Close();

                return window;
            });

            services.AddTransient(s =>
            {
                var model = s.GetRequiredService<EditPostWindowViewModel>();
                var window = new EditPostWindow() { DataContext = model };
                model.DialogComplete += (_, _) => window.Close();

                return window;
            });

            services.AddTransient(s =>
            {
                var model = s.GetRequiredService<AddEmployeeWindowViewModel>();
                var window = new AddEmployeeWindow() { DataContext = model };
                model.DialogComplete += (_, _) => window.Close();

                return window;
            });

            services.AddTransient(s =>
            {
                var model = s.GetRequiredService<EditEmployeeWindowViewModel>();
                var window = new EditEmployeeWindow() { DataContext = model };
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
