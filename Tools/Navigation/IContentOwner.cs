using System.Windows.Controls;

namespace YatskivLab4.Tools.Navigation
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}
