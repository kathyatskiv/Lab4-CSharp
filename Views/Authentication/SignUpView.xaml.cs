using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels.Authentication;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Views.Authentication
{
    public partial class SignUpView : UserControl, INavigatable
    {
        internal SignUpView()
        {
            InitializeComponent();
            DataContext = new SignUpViewModel();
        }
    }
}
