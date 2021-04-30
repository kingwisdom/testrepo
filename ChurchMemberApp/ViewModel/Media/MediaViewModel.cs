using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Media;
using ChurchMemberApp.Views.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Media
{ 
    public class MediaViewModel : BaseViewModel
    {
        public MediaViewModel()
        {
            
            AllMedia = new ObservableCollection<ChurchMedia>();
            DisplayMedia();
        }

        ObservableCollection<ChurchMedia> allMedia;
        public ObservableCollection<ChurchMedia> AllMedia
        {
            get { return allMedia; }
            set
            {
                allMedia = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ChurchMedia> _Audio;
        public ObservableCollection<ChurchMedia> Audio
        {
            get { return _Audio; }
            set { _Audio = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ChurchMedia> _allAudio;
        public ObservableCollection<ChurchMedia> AllAudio
        {
            get { return _allAudio; }
            set { _allAudio = value; OnPropertyChanged(); }
        }
        
        private ObservableCollection<ChurchMedia> _allbooks;
        public ObservableCollection<ChurchMedia> AllBooks
        {
            get { return _allbooks; }
            set { _allbooks = value; OnPropertyChanged(); }
        }
        
        private ObservableCollection<ChurchMedia> _video;
        public ObservableCollection<ChurchMedia> Video
        {
            get { return _video; }
            set { _video = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ChurchMedia> _allvideo;
        public ObservableCollection<ChurchMedia> AllVideo
        {
            get { return _allvideo; }
            set { _allvideo = value; OnPropertyChanged(); }
        }

        private ChurchMedia _first;

        public ChurchMedia CurrentMessage
        {
            get { return _first; }
            set { _first = value;  OnPropertyChanged(); }
        }

        private ChurchMedia _selectItem;

        public ChurchMedia SelectItem
        {
            get { return _selectItem; }
            set { _selectItem = value; }
        }

        public Command<ChurchMedia> PlayTapped => new Command<ChurchMedia>(async (media) =>
        {
             if (media == null)
                 return;
             
             await App.Current.MainPage.Navigation.PushAsync(new PlayAudioPage(media, "dd"));
             MessagingCenter.Send(this, "Stream", media);
         });
        
        public Command<ChurchMedia> PlayVideoTapped => new Command<ChurchMedia>(async (media) =>
        {
             if (media == null)
                 return;
             
             await App.Current.MainPage.Navigation.PushAsync(new VLCPage(media));
         });

        public ICommand SeeAllCommand => new Command(async() =>
        {
            await App.Current.MainPage.Navigation.PushAsync(new MoreAudioPage());
        });
        public ICommand SeeAllVideoCommand => new Command(async() =>
        {
            await App.Current.MainPage.Navigation.PushAsync(new MoreVideoPage());
        });
        
        public ICommand SeeAllBooksCommand => new Command(async() =>
        {
            await App.Current.MainPage.Navigation.PushAsync(new MoreBooksPage());
        });
        private async void DisplayMedia()
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    AllMedia = null;
                    IsBusy = false;
                    return;
                }
                var response = await ApiServices.GetAllChurchMedia();
                if (response != null)
                {
                    foreach (var item in response)
                    {
                        AllMedia.Add(item);
                    }
                    AllAudio = new ObservableCollection<ChurchMedia>(AllMedia.Where(er => er.mediaType == MediaType.Audio || er.mediaType == MediaType.Ebook));
                    AllVideo = new ObservableCollection<ChurchMedia>(AllMedia.Where(er => er.mediaType == MediaType.Video));
                    AllBooks = new ObservableCollection<ChurchMedia>(AllMedia.Where(er => er.mediaType == MediaType.Ebook));
                    
                    Audio = new ObservableCollection<ChurchMedia>(AllMedia.Where(er => er.mediaType == MediaType.Audio).Take(4));
                    Video = new ObservableCollection<ChurchMedia>(AllMedia.Where(er => er.mediaType == MediaType.Video).Take(4));
                   // Video = AllMedia.Where(er => er.mediaType == 3);
                    CurrentMessage = AllMedia.First();
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {

                MessageDialog.Show("Error!", ex.Message, "Cancel");
            }
        }

      

    }
}
