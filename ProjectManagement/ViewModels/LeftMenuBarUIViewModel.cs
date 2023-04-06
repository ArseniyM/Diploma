using ProjectManagement.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagement.ViewModels
{
    class LeftMenuBarUIViewModel: ViewModel
    {
        #region Поля

        #region StatusMenu: Bool - Состояние меню (true - развернуто, false - свернуто)
        private bool _StatusMenu;

        public bool StatusMenu
        {
            get => _StatusMenu;
            set => Set(ref _StatusMenu, value);
        }
        #endregion

        #endregion

        #region Команды

        public ICommand EditStatusMenuCommand { get; }
        private void OnEditStatusMenuCommandExecuted(object p)
        {
            StatusMenu = !StatusMenu;
        }
        private bool CanEditStatusMenuCommandExecute(object p) => true;

        #endregion

        #region Конструктор
        public LeftMenuBarUIViewModel()
        {
            StatusMenu = false;
            EditStatusMenuCommand = new RelayCommand(OnEditStatusMenuCommandExecuted, CanEditStatusMenuCommandExecute);
        }
        #endregion
    }
}
