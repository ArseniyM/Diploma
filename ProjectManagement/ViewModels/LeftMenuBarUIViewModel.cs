using ProjectManagement.Infrastructure.Commands;
using ProjectManagement.Services;
using ProjectManagement.Services.NavigatorPages;
using ProjectManagement.ViewModels.Base;
using System.ComponentModel;
using System.Windows.Input;

namespace ProjectManagement.ViewModels
{
    class LeftMenuBarUIViewModel: ViewModel
    {
        #region Поля

        public static readonly string OrganizationalStructurePageViewModelAlias = "OrganizationalStructurePage";
        public static readonly string NotFoundPageViewModelAlias = "Page404ViewModel";



        private readonly IViewModelsResolver _resolver;


        private readonly INotifyPropertyChanged _organizationalStructurePageViewModel;
        public INotifyPropertyChanged OrganizationalStructurePage
        {
            get => _organizationalStructurePageViewModel;
        }

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

        public ICommand GoToPageOrgStractCommand { get; }
        private void OnGoToPageOrgStractCommandExecuted(object p)
        {
            Navigation.Navigate(Navigation.OrganizationalStructurePageAlias, OrganizationalStructurePage);
        }
        private bool CanGoToPageOrgStractCommandExecute(object p) => true;

        #endregion

        #region Конструктор
        public LeftMenuBarUIViewModel()
        {
            StatusMenu = false;
            EditStatusMenuCommand = new RelayCommand(OnEditStatusMenuCommandExecuted, CanEditStatusMenuCommandExecute);
            GoToPageOrgStractCommand = new RelayCommand(OnGoToPageOrgStractCommandExecuted, CanGoToPageOrgStractCommandExecute);
        }

        public LeftMenuBarUIViewModel(IViewModelsResolver viewModelsResolver) : this()
        {
            _resolver = viewModelsResolver;

            _organizationalStructurePageViewModel = _resolver.GetViewModelInstance(OrganizationalStructurePageViewModelAlias);
        }
        #endregion
    }
}
