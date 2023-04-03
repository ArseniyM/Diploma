﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.ViewModels
{
    class MainWindowViewModel: ViewModel
    {
        #region Поля
        #region Title: String - Заголовок окна
        private string _Title = "Главное окно";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Background: String - Цвет фона окна
        private string _Background = "#A1A3B4";

        public string Background
        {
            get => _Background;
            set => Set(ref _Background, value);
        }
        #endregion
        #endregion

    }
}
