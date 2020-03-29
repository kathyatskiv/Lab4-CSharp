using YatskivLab4.Tools.Navigation;
using YatskivLab4.ViewModels;
namespace YatskivLab4.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class Main : INavigatable
    {
        public Main()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
