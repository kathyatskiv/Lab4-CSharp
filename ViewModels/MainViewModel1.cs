using System.Windows;
using System.Windows.Input;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private Visibility _menuVisibility = Visibility.Collapsed;
        private ICommand _showMenuCommand;
        private ICommand _logOutCommand;
        private ICommand _closeCommand;

        public string CurrentUser
        {
            get
            {
                return $"Current User: {StationManager.CurrentUser}";
            }
        }

        public Visibility MenuVisibility
        {
            get { return _menuVisibility; }
            private set
            {
                _menuVisibility = value; 
                OnPropertyChanged();
            }
        }

        public ICommand ShowMenuCommand
        {
            get { return _showMenuCommand ?? (_showMenuCommand = new RelayCommand<object>(ShowMenuImplementation)); }
        }
        public ICommand LogOutCommand
        {
            get { return _logOutCommand ?? (_logOutCommand = new RelayCommand<object>(LogOutImplementation)); }
        }
        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseImplementation)); }
        }

        private void ShowMenuImplementation(object obj)
        {
            MenuVisibility = _menuVisibility==Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void LogOutImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.SignIn);
        }

        private void CloseImplementation(object obj)
        {
            StationManager.CloseApp();
        }
    }
}
