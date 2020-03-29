using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Properties;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels
{
    class UserListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _users;
        private Thread _workingThread;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private BackgroundWorker _backgroundWorker;
        private Task _backgroundTask;

        public ObservableCollection<User> Users
        {
            get => _users;
            private set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        internal UserListViewModel()
        {
            _users = new ObservableCollection<User>(StationManager.DataStorage.UsersList);
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            StartWorkingThread();
            StationManager.StopThreads += StopWorkingThread;
            //StartBackgroundWorker();
            //StationManager.StopThreads += StopBackgroundWorker;
            //StartBarckgroundTask();
            //StationManager.StopThreads += StopBackgroundTask;
        }

        private void StartWorkingThread()
        {
            _workingThread = new Thread(WorkingThreadProcess);
            _workingThread.Start();
        }

        private void StartBackgroundWorker()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += BackgroundWorkerProcess;
            _backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
            _backgroundWorker.RunWorkerAsync();
        }

        private void StartBarckgroundTask()
        {
            _backgroundTask = Task.Factory.StartNew(BackgroundTaskProcess, TaskCreationOptions.LongRunning);
        }

        private void WorkingThreadProcess()
        {
            int i = 0;
            while (!_token.IsCancellationRequested)
            {
                var users = _users.ToList();
                users.Add(new User("FirstNAme" + i, "LastNAme" + i, "Email" + i, "Login" + i, "Password" + i));
                LoaderManager.Instance.ShowLoader();
                Users = new ObservableCollection<User>(users);
                for (int j = 0; j < 3; j++)
                {
                    Thread.Sleep(500);
                    if (_token.IsCancellationRequested)
                        break;
                }
                if (_token.IsCancellationRequested)
                    break;
                LoaderManager.Instance.HideLoader();
                for (int j = 0; j < 10; j++)
                {
                    Thread.Sleep(500);
                    if (_token.IsCancellationRequested)
                        break;
                }
                if (_token.IsCancellationRequested)
                    break;
                i++;
            }
        }

        private void BackgroundWorkerProcess(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var worker = (BackgroundWorker)sender;
            int i = 0;
            while (!worker.CancellationPending)
            {
                var users = _users.ToList();
                users.Add(new User("FirstNAme" + i, "LastNAme" + i, "Email" + i, "Login" + i, "Password" + i));
                worker.ReportProgress(10, users);
                for (int j = 0; j < 10; j++)
                {
                    Thread.Sleep(500);
                    if (worker.CancellationPending)
                    {
                        doWorkEventArgs.Cancel = true;
                        _tokenSource.Cancel();
                        break;
                    }
                }
                i++;
            }
        }
        private async void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Users = new ObservableCollection<User>((List<User>) progressChangedEventArgs.UserState);
                Thread.Sleep(2000);
            });
            LoaderManager.Instance.HideLoader();
        }

        private void BackgroundTaskProcess()
        {
            int i = 0;
            while (!_token.IsCancellationRequested)
            {
                var users = _users.ToList();
                users.Add(new User("FirstNAme" + i, "LastNAme" + i, "Email" + i, "Login" + i, "Password" + i));
                LoaderManager.Instance.ShowLoader();
                Users = new ObservableCollection<User>(users);
                for (int j = 0; j < 3; j++)
                {
                    Thread.Sleep(500);
                    if (_token.IsCancellationRequested)
                        break;
                }
                if (_token.IsCancellationRequested)
                    break;
                LoaderManager.Instance.HideLoader();
                for (int j = 0; j < 10; j++)
                {
                    Thread.Sleep(500);
                    if (_token.IsCancellationRequested)
                        break;
                }
                if (_token.IsCancellationRequested)
                    break;
                i++;
            }
        }

        internal void StopWorkingThread()
        {
            _tokenSource.Cancel();
            _workingThread.Join(2000);
            _workingThread.Abort();
            _workingThread = null;
        }

        internal void StopBackgroundWorker()
        {
            _backgroundWorker.CancelAsync();
            for (int i = 0; i < 4; i++)
            {
                if (_token.IsCancellationRequested)
                    break;
                Thread.Sleep(500);
            }
            _backgroundWorker.Dispose();
            _backgroundWorker = null;
        }

        internal void StopBackgroundTask()
        {
            _tokenSource.Cancel();
            _backgroundTask.Wait(2000);
            _backgroundTask.Dispose();
            _backgroundTask = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
