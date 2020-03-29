using System;
using System.Windows;
using System.Windows.Controls;
using YatskivLab4.Models;
using YatskivLab4.Tools.DataStorage;
using YatskivLab4.ViewModels;

namespace YatskivLab4.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;
        internal static Person CurrentPerson { get; set; }
        internal static Person TempPerson { get; set; }
        internal static DataGrid PersonGrid { get; set; }

        internal static EditPersonViewModel EditPersonVM { get; set; }
        internal static MainViewModel MainVM { get; set; }

        internal static IDataStorage DataStorage { get; private set; }

        internal static void Initialize(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("Closing the app...");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
