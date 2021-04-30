using ChurchMemberApp.Models;
using ChurchMemberApp.ViewModel.Media;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Giving;
using ChurchMemberApp.Views.Pages;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.Views.Media
{
    public partial class LiveView : ContentView
    {
        YoutubeViewModel vm;
        public LiveView()
        {
            InitializeComponent();
            BindingContext = vm = new YoutubeViewModel();
           // webView.Source = $"https://www.youtube.com/watch?v=4150Ebz25pc";
            webView.Source = $"https://thepottershouseoflagos.org/watch-live/";
            loading.IsVisible = false;
        }

        //private async void videofeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var f = e.CurrentSelection[0] as Youtube;
        //    var sFeed = new LiveDetailPage(f.VideoId);
        //    await Navigation.PushAsync(sFeed);

        //    ((CollectionView)sender).SelectedItem = null;
        //}

        //private void play_Tapped(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new YoutubeLive());
        //}

        private async void give_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LiveGivePage());
        }

        private async void mark_Tapped(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                await Navigation.PushAsync(new LoginPage());
                return;
            }

            await Navigation.PushAsync(new AttendanceWithBarcodePage());
        }

        private void video_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var f = e.CurrentSelection[0] as Youtube;
            //var sFeed = new LiveDetailPage(f.VideoId);
            //await Navigation.PushAsync(sFeed);
            webView.Source = $"https://www.youtube.com/watch?v={f.VideoId}";
           // ((CollectionView)sender).SelectedItem = null;
        }

        protected async void OnDissapearing()
        {
            OnDissapearing();
            await Navigation.PopAsync();
        }

        private async void post_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePostPage());
        }
    }
}
