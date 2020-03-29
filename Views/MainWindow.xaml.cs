using System.Windows.Controls;
using YatskivLab4.Tools.DataStorage;
using YatskivLab4.Tools.Managers;
using YatskivLab4.Tools.Navigation;
using YatskivLab4.ViewModels;

namespace YatskivLab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IContentOwner
    {
        public ContentControl ContentControl => _contentControl;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.Main);
        }
    }
}
