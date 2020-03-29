
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using YatskivLab4.Models;
using YatskivLab4.Tools;
using YatskivLab4.Tools.Managers;
using YatskivLab4.Tools.Navigation;

namespace YatskivLab4.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Fields
        private List<Person> _personsList = StationManager.DataStorage.PersonsList;
        private Person _selectedPerson;
        private int _sortIndex;
        private int _filterIndex;
        private string[] _sortList = { "Name", "Surname", "Email", "Birthday", "SunSign", "ChineseSign" };
        private string[] _filterList = { "Name", "Surname", "Email", "SunSign", "ChineseSign" };
        #endregion

        #region Commands
        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        private RelayCommand<object> _saveCommand;
        private RelayCommand<object> _filterCommand;
        #endregion

        #region Properties

        public MainViewModel()
        {
            StationManager.MainVM = this;
        }

        public string FilterWord { get; set; }

        public int SortIndex
        {
            get => _sortIndex;
            set
            {
                _sortIndex = value;

                UpdateList();
            }
        }

        public int FilterIndex
        {
            get => _filterIndex;
            set
            {
                _filterIndex = value;

                UpdateList();
            }
        }

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;

                OnPropertyChanged();
                StationManager.CurrentPerson = _selectedPerson;
            }
        }

        public IEnumerable<Person> MyPersonsList
        {
            get
            {
                IEnumerable<Person> list = _personsList;
                switch (SortIndex)
                {
                    case 0: list = list.OrderBy(p => p.Name); break;
                    case 1: list = list.OrderBy(p => p.Surname); break;
                    case 2: list = list.OrderBy(p => p.Email); break;
                    case 3: list = list.OrderBy(p => p.Birthday); break;
                    case 4: list = list.OrderBy(p => p.SunSign); break;
                    case 5: list = list.OrderBy(p => p.ChineseSign); break;
                }

                if (String.IsNullOrWhiteSpace(FilterWord)) return list;

                switch (FilterIndex)
                {
                    case 0: list = list.Where(p => p.Name.Contains(FilterWord)); break;
                    case 1: list = list.Where(p => p.Surname.Contains(FilterWord)); break;
                    case 2: list = list.Where(p => p.Email.Contains(FilterWord)); break;
                    case 3: list = list.Where(p => p.SunSign.Contains(FilterWord)); break;
                    case 4: list = list.Where(p => p.ChineseSign.Contains(FilterWord)); break;
                }

                return list;
            }
        }

        public IEnumerable<string> SortList => _sortList;

        public IEnumerable<string> FilterList => _filterList;

        #endregion

        #region Commands

        public RelayCommand<object> AddPersonCommand => _addPersonCommand ?? (_addPersonCommand = new RelayCommand<object>(o => AddPersonImplementation()));

        public RelayCommand<object> EditPersonCommand => _editPersonCommand ?? (_editPersonCommand = new RelayCommand<object>(o => EditPersonImplementation(), CanExecute));

        public RelayCommand<object> SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand<object>(o => SaveImplementation()));

        public RelayCommand<object> DeletePersonCommand => _deletePersonCommand ?? (_deletePersonCommand = new RelayCommand<object>(o => DeletePersonImplementation(), CanExecute));

        public RelayCommand<object> FilterCommand => _filterCommand ?? (_filterCommand = new RelayCommand<object>(o => { UpdateList(); }));

        #endregion

        #region CommandImplementation

        private void AddPersonImplementation()
        {
            StationManager.CurrentPerson = new Person("", "", "");
            NavigationManager.Instance.Navigate(ViewType.AddPerson);
        }

        private void EditPersonImplementation()
        {
            StationManager.CurrentPerson = SelectedPerson;
            StationManager.TempPerson = new Person(StationManager.CurrentPerson.Name, StationManager.CurrentPerson.Surname, StationManager.CurrentPerson.Email, StationManager.CurrentPerson.Birthday);
            StationManager.EditPersonVM.UpdatePersons();
            NavigationManager.Instance.Navigate(ViewType.EditPerson);
        }

        private async void DeletePersonImplementation()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                StationManager.DataStorage.DeletePerson(SelectedPerson);
                OnPropertyChanged(nameof(MyPersonsList));
            });
            LoaderManager.Instance.HideLoader();
        }

        private void SaveImplementation()
        {
            StationManager.DataStorage.SaveChanges();
        }

        #endregion

        public bool CanExecute(object obj) => SelectedPerson != null;

        public void UpdateList() { OnPropertyChanged(nameof(MyPersonsList)); }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

       // [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
