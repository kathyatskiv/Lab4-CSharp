namespace YatskivLab4.Tools.Navigation
{
    internal enum ViewType
    {
        Main,
        AddPerson,
        EditPerson
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
