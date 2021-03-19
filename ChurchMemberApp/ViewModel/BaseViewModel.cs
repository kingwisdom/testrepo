using ChurchMemberApp.Core;
using ChurchMemberApp.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        private bool isNotBusy;
        public bool IsNotBusy
        {
            get { return isNotBusy; }
            set { isNotBusy = value; OnPropertyChanged(); }
        }

        bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(); }
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        #region services
        //ChatService chatService;
        //public ChatService ChatService =>
        //    chatService ?? (chatService = DependencyService.Resolve<ChatService>());

        //IDialogService dialogService;
        //public IDialogService DialogService =>
        //    dialogService ?? (dialogService = DependencyService.Resolve<IDialogService>());
        #endregion

    }
}
