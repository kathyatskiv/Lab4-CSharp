using YatskivLab4.Tools.Navigation;
using YatskivLab4.ViewModels;

namespace YatskivLab4.Views
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddPerson : INavigatable
    {
        public AddPerson()
        {
            InitializeComponent();
            DataContext = new AddPersonViewModel();
        }
    }
}
