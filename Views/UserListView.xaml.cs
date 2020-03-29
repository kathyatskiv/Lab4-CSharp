using System;
using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Views
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView : UserControl
    {
        public UserListView()
        {
            InitializeComponent();
            DataContext = new UserListViewModel();
        }
    }
}
