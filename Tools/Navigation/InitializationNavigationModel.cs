using System;
using Main = YatskivLab4.Views.Main;
using AddPerson = YatskivLab4.Views.AddPerson;
using EditPerson = YatskivLab4.Views.EditPerson;
using YatskivLab4.Views;

namespace YatskivLab4.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {
            
        }
   
        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.AddPerson:
                    ViewsDictionary.Add(viewType,new AddPerson());
                    break;
                case ViewType.EditPerson:
                    ViewsDictionary.Add(viewType, new EditPerson());
                    break;
                case ViewType.Main:
                    ViewsDictionary.Add(viewType, new Main());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
