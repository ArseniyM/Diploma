using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.ViewModels
{
    internal class OrganizationalStructurePageViewModel: ViewModel
    {
        private String _background = "red";
        public String Background
        {
            get => _background;
            set => Set(ref _background, value);
        }

        public OrganizationalStructurePageViewModel()
        {
            Background = "red";
        }
    }
}
