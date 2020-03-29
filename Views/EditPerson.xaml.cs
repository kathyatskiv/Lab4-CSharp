using YatskivLab4.Tools.Navigation;
using YatskivLab4.ViewModels;

namespace YatskivLab4.Views
{
    /// <summary>
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditPerson : INavigatable
    {
        public EditPerson()
        {
            InitializeComponent();
            DataContext = new EditPersonViewModel();
        }
    }
}
