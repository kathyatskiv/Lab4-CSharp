using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatskivLab4.Tools.Managers;
using YatskivLab4.Models;
using YatskivLab4.Tools;
using YatskivLab4.Tools.Navigation;
using System.Runtime.CompilerServices;
using YatskivLab4.Tools.Exceptions;
using System.Windows;

namespace YatskivLab4.ViewModels
{
    class EditPersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = StationManager.CurrentPerson;
        private Person _tempPerson = StationManager.TempPerson;
        private Person _delPerson;
        #endregion
        #region Commands
        private RelayCommand<object> _proceedCommand;
        private RelayCommand<object> _backCommand;
        #endregion

        #region Properties

        public EditPersonViewModel()
        {
            StationManager.EditPersonVM = this;
        }

        public Person Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public Person TempPerson
        {
            get => _tempPerson;
            set
            {
                _tempPerson = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand<object> ProceedCommand =>
            _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(o => ProceedImplementation(), CanExecute));

        public RelayCommand<object> BackCommand =>
            _backCommand ?? (_backCommand = new RelayCommand<object>(o => BackImplementation(), CanExecute));

        #endregion

        private async void ProceedImplementation()
        {
            LoaderManager.Instance.ShowLoader();
            bool succes = await Task.Run(() =>
            {
                try
                {
                    TempPerson.NameValidation(Person.Name);
                }
                catch (InvalidNameException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                try
                {
                    TempPerson.SurnameValidation(Person.Name);
                }
                catch (InvalidSurnameException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                try
                {
                    TempPerson.EmailValidation(Person.Email);
                }
                catch (InvalidEmailException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                try
                {
                    TempPerson.BirthdayValidation(Person.Birthday);
                }
                catch (NotBornPersonException e)
                {
                    MessageBox.Show($"Not correct age: {e.Message}");
                    return false;
                }
                catch (DeadPersonException e)
                {
                    MessageBox.Show($"Not correct age: {e.Message}");
                    return false;
                }

                if (Person.IsBirthday)
                {
                    MessageBox.Show($"Happy Birthday, {Person.Name}!");
                }

                return true;
            });
            LoaderManager.Instance.HideLoader();

            if (succes)
            {
                Person _delperson = _person;
                Person = TempPerson;
                StationManager.TempPerson = null;
                StationManager.DataStorage.AddPerson(_person);
                StationManager.DataStorage.DeletePerson(_delperson);
                StationManager.MainVM.updatePersonList();
                StationManager.DataStorage.SaveChanges();
                StationManager.MainVM.UpdateList();
                NavigationManager.Instance.Navigate(ViewType.Main);
            }
        }

        private void BackImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Main);
        }

        private bool CanExecute(object obj) =>
            !String.IsNullOrEmpty(Person.Name) &&
            !String.IsNullOrEmpty(Person.Surname) &&
            !String.IsNullOrEmpty(Person.Email);

        public void UpdatePersons()
        {
            TempPerson = StationManager.TempPerson;
            _person = StationManager.CurrentPerson;
            OnPropertyChanged("TempPerson");
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
