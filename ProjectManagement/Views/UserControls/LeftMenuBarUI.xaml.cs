using ProjectManagement.Services;
using ProjectManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectManagement.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для LeftMenuBarUI.xaml
    /// </summary>
    public partial class LeftMenuBarUI : UserControl
    {
        public LeftMenuBarUI()
        {
            InitializeComponent();

            Loaded += LeftMenuBarUI_Loaded;
        }

        private void LeftMenuBarUI_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new LeftMenuBarUIViewModel(new ViewModelsResolver());
        }
    }
}
