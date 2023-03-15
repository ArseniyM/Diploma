using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.ViewModels
{
    class AuthorizationWindowViewModel : ViewModel
    {
        #region Заголовок окна
        private string _Title = "Авторизация";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Цвет фона окна
        private string _Background = "#46486C";

        public string Background
        {
            get => _Background;
            set => Set(ref _Background, value);
        }
        #endregion
    }
}
