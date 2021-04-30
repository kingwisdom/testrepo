using ChurchMemberApp.Models;
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
    public partial class AudioList : ContentPage
    {
        public AudioList()
        {
            InitializeComponent();
            BindingContext = new AudioViewModel();
        }

        private async void audio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var page = e.CurrentSelection[0] as DownloadedMedia;
            //var item = new MediaDownloadDetailPage(page);
           // await Navigation.PushAsync(item);

            if (page.mediaType == Models.Response.MediaType.Video)
            {
                await Navigation.PushAsync(new VLCPage(page.filePath));
                return;
            }
            await Navigation.PushAsync(new PlayAudio(page, "dd"));
            MessagingCenter.Send(this, "Stream", page);
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}