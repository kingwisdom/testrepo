using ChurchMemberApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Media
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LiveDetailPage : ContentPage
    {
        public LiveDetailPage()
        {
            InitializeComponent();
        }
        public LiveDetailPage(string videoId)
        {
            InitializeComponent();
            BindingContext = new Youtube();
            App.videourl = "https://www.youtube.com/embed/" + videoId;


            App.videoView = youtubeVideo;
            //  this.Content = App.videoView;
            App.videoView.Source = App.videourl;
            App.videostart = true;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await progressBar.ProgressTo(0.9, 900, Easing.SpringIn);

        }
        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            progressBar.IsVisible = true;
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            progressBar.IsVisible = false;
        }
    }
}