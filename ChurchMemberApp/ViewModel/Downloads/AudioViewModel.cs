using ChurchMemberApp.Models;
using ChurchMemberApp.Views.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChurchMemberApp.ViewModel.Downloads
{
    public class AudioViewModel : BaseViewModel
    {
        public AudioViewModel()
        {
            AllMedia = new ObservableCollection<DownloadedMedia>();
            DisplayMedia();
        }

        
        ObservableCollection<DownloadedMedia> allMedia;
        public ObservableCollection<DownloadedMedia> AllMedia
        {
            get { return allMedia; }
            set
            {
                allMedia = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<DownloadedMedia> _allAudio;
        public ObservableCollection<DownloadedMedia> AllAudio
        {
            get { return _allAudio; }
            set { _allAudio = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DownloadedMedia> _allvideo;
        public ObservableCollection<DownloadedMedia> AllVideo
        {
            get { return _allvideo; }
            set { _allvideo = value; OnPropertyChanged(); }
        }

        private DownloadedMedia _selectItem;

        public DownloadedMedia SelectItem
        {
            get { return _selectItem; }
            set { _selectItem = value; }
        }

        private async void DisplayMedia()
        {
            IsBusy = true;

            try
            {
                var response = await App.Database.GetDownloadedAudio();
                if (response != null)
                {
                    foreach (var item in response)
                    {
                        AllMedia.Add(item);
                    }
                    AllAudio = new ObservableCollection<DownloadedMedia>(AllMedia.Where(er => er.mediaType == Models.Response.MediaType.Audio));
                    AllVideo = new ObservableCollection<DownloadedMedia>(AllMedia.Where(er => er.mediaType == Models.Response.MediaType.Video));

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
