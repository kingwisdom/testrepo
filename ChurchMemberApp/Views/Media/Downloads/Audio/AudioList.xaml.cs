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
            var item = new MediaDownloadDetailPage(e.CurrentSelection[0] as DownloadedMedia);
            await Navigation.PushAsync(item);
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}