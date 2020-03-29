using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YatskivLab4.Models;
using YatskivLab4.Tools;
using YatskivLab4.Tools.Exceptions;
using YatskivLab4.Tools.Managers;
using YatskivLab4.Tools.Navigation;

namespace YatskivLab4.ViewModels
{
    class AddPersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = StationManager.CurrentPerson;
        #endregion

        #region Commands
        private RelayCommand<object> _proceedCommand;
        #endregion

        #region Properties

        public Person Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand<object> ProceedCommand =>
            _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(ProceedImplementation, CanExecute));

        #endregion

        private async void ProceedImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            bool succes = await Task.Run(() =>
            {
                try
                {
                    Person.NameValidation(Person.Name);
                }
                catch (InvalidNameException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                try
                {
                    Person.SurnameValidation(Person.Name);
                }
                catch (InvalidSurnameException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                try
                {
                    Person.EmailValidation(Person.Email);
                }
                catch (InvalidEmailException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                try
                {
                    Person.BirthdayValidation(Person.Birthday);
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
                StationManager.DataStorage.AddPerson(_person);
                _person = new Person("", "", "");
                Person = _person;
                StationManager.MainVM.UpdateList();
                NavigationManager.Instance.Navigate(ViewType.Main);
            }

        }

        private bool CanExecute(object obj) =>
            !String.IsNullOrEmpty(Person.Name) &&
            !String.IsNullOrEmpty(Person.Surname) &&
            !String.IsNullOrEmpty(Person.Email);

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
