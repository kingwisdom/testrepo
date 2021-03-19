using ChurchMemberApp.Models;
using ChurchMemberApp.Platform;
using ChurchMemberApp.ViewModel.Downloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Media.Downloads.Audio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediaDownloadDetailPage : ContentPage
    {
        public long bytes_total { get; set; }
        public long Size { get; set; }
        IFileHelper dependency { get; set; }
        Imessaging message { get; set; }

        public MediaDownloadDetailPage()
        {
            InitializeComponent();
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        DownloadedMedia media;

        public MediaDownloadDetailPage(DownloadedMedia model)
        {
            InitializeComponent();
            this.BindingContext = new MediaDownloadDetailVM(model);
            media = model;
        }

        private async void play_Tapped(object sender, EventArgs e)
        {
            if (media.mediaType == Models.Response.MediaType.Video)
            {
                //await Navigation.PushAsync(new PlayVideoPage(media));
                return;
            }
            await Navigation.PushAsync(new PlayAudio(media, "dd"));
            MessagingCenter.Send(this, "Stream", media);
        }
    }
}