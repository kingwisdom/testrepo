
using LibVLCSharp.Shared;
using MediaManager;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace ChurchMemberApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class test : ContentPage
    {
        LibVLC lib;
        public test()
        {
            InitializeComponent();
           // Init();
        }

        public test(string url)
        {
            InitializeComponent();
            Init(url);
        }



        private async void Init(string url)
        {
           await CrossMediaManager.Current.Play(url);
        }

    }
}