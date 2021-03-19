using ChurchMemberApp.Models;
using ChurchMemberApp.ViewModel.Downloads;
using ChurchMemberApp.ViewModel.Media;
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
    public partial class PlayAudio : ContentPage
    {
        public PlayAudio()
        {
            InitializeComponent();
        }
        
        public PlayAudio(DownloadedMedia media, string v)
        {
            InitializeComponent();
            BindingContext = new PlayAudioVM(media, v);
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}