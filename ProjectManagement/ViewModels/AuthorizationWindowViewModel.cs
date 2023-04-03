using ProjectManagement.Infrastructure.Commands;
using ProjectManagement.Views.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjectManagement.ViewModels
{
    class AuthorizationWindowViewModel : ViewModel
    {
        #region Поля
        #region Title: String - Заголовок окна
        private string _Title = "Авторизация";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Login: String - Логин
        private string _Login = "";

        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }
        #endregion

        #region Background: String - Цвет фона окна
        private string _Background = "#46486C";

        public string Background
        {
            get => _Background;
            set => Set(ref _Background, value);
        }
        #endregion
        #endregion

        #region Команды

        #region AuthorizationSystemCommand - команда авторизации в системе
        public ICommand AuthorizationSystemCommand { get; }
        private void OnAuthorizationSystemCommandExecuted(object p)
        {
            String pas = ((PasswordBox)p).Password;
            using (ProjectManagementContext db = new ProjectManagementContext())
            {
                if (db.Employees.FirstOrDefault(e => e.Login == Login) != null)
                {
                    if (db.Employees.FirstOrDefault(e => e.Login == Login && e.Password == pas) != null)
                    {
                        MessageBox.Show("Вход выполнен успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow window = new MainWindow();
                        window.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь с логином \""+Login+"\" не найден!","Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private bool CanAuthorizationSystemCommandExecute(object p) => (Login != "" && ((PasswordBox)p).Password != "");
        #endregion
        
        #endregion

        #region Конструктор

        public AuthorizationWindowViewModel()
        {
            #region Команды

            AuthorizationSystemCommand = new RelayCommand(OnAuthorizationSystemCommandExecuted, CanAuthorizationSystemCommandExecute);

            #endregion
        }

        #endregion
    }
}
