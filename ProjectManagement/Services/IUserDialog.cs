﻿namespace ProjectManagement.Services
{
    public interface IUserDialog
    {
        void OpenMainWindow();
        void OpenNewEmployeeWindow();
        void OpenAuthorizationWindow();
        void OpenAddDepartmentWindow();
        void OpenEditDepartmentWindow();
        void OpenAddPostWindow();
        void OpenEditPostWindow();
        void OpenAddEmployeeWindow();
        void OpenEditEmployeeWindow();
        void OpenAddProjectWindow();
    }
}
