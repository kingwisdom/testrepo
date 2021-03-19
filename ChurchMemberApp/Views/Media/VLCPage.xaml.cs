using LibVLCSharp.Shared;
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
    public partial class VLCPage : ContentPage
    {
        LibVLC lib;
        public VLCPage(string path)
        {
            InitializeComponent();

            LibVLCSharp.Shared.Core.Initialize();
            lib = new LibVLC();

            var media = new LibVLCSharp.Shared.Media(lib, path,FromType.FromLocation);
            mymedia.MediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true };
            mymedia.MediaPlayer.Play();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            mymedia.MediaPlayer.Stop();
        }
    }
}