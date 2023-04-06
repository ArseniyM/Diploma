using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services
{
    public interface IUserDialog
    {
        void OpenMainWindow();
        void OpenAuthorizationWindow();
    }
}
