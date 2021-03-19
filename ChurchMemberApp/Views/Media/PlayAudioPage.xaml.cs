using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Media;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChurchMemberApp.Views.Media
{
    public partial class PlayAudioPage : ContentPage
    {
        private ChurchMedia media;
        private string v;
        //private DownloadedAudio item;

        public AudioPlayerViewModel Vm => BindingContext as AudioPlayerViewModel;

        public PlayAudioPage(ChurchMedia media, string v)
        {
            InitializeComponent();
            BindingContext = new AudioPlayerViewModel(media, v);
        }

        //public PlayAudioPage(DownloadedAudio item)
        //{
        //    InitializeComponent();
        //    BindingContext = new ViewModels.Media.AudioPlayerViewModel(item);
        //}
        public PlayAudioPage()
        {
            InitializeComponent();
        }

        private async void back_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
