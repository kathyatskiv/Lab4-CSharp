using System.Collections.Generic;

namespace YatskivLab4.Tools.Navigation
{
    internal abstract class BaseNavigationModel : INavigationModel
    {
        private readonly IContentOwner _contentOwner;
        private readonly Dictionary<ViewType, INavigatable> _viewsDictionary;

        protected BaseNavigationModel(IContentOwner contentOwner)
        {
            _contentOwner = contentOwner;
            _viewsDictionary = new Dictionary<ViewType, INavigatable>();
        }

        protected IContentOwner ContentOwner => _contentOwner;

        protected Dictionary<ViewType, INavigatable> ViewsDictionary => _viewsDictionary;

        public void Navigate(ViewType viewType)
        {
            if (!ViewsDictionary.ContainsKey(viewType))
                InitializeView(viewType);

            ContentOwner.ContentControl.Content = ViewsDictionary[viewType];
        }

        protected abstract void InitializeView(ViewType viewType);

    }
}
