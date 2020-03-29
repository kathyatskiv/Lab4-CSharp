using System.ComponentModel;
using System.Runtime.CompilerServices;
//using YatskivLab4.Properties;

namespace YatskivLab4.Tools
{
    internal abstract class BaseViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

 //       [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
