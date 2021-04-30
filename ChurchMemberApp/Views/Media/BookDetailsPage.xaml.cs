using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Media
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailsPage : ContentPage
    {
        public BookDetailsPage()
        {
            InitializeComponent();
        }

        public long bytes_total { get; set; }

        public long Size { get; set; }
        Imessaging message { get; set; }
        IFileHelper dependency { get; set; }
        //public float Progress { get; set; }

        public float DownloadedSize { get; set; } 

        ChurchMedia media;
        public BookDetailsPage(ChurchMedia m)
        {
            InitializeComponent();
            media = m;
            BindingContext = media;
        }

        private void download_Clicked(object sender, EventArgs e)
        {
            message = DependencyService.Get<Imessaging>();
           // var isusersignedin = Preferences.Get(App.USERSIGNEDINKEY, false);
           // var hasactivesubscription = Preferences.Get(App.HASACTIVESUBSCRIPTION, false);
            try
            {
                indicator.IsRunning = true;
                download.IsVisible = false;
                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    message.LongAlert("No Internet Connection");
                }

                progress.IsVisible = true;
                dependency = DependencyService.Get<IFileHelper>();
                message = DependencyService.Get<Imessaging>();
                var filename = media.id;
                var all = App.Database.GetDownloadedAudio().Result.FirstOrDefault(x => x.id == filename);
                if (all != null)
                {
                    message.LongAlert("This Ebook has already been downloaded, please check you Downloads");
                    indicator.IsRunning = false;
                    download.IsVisible = true;
                    return;
                }
                WebClient client = new WebClient();
                var dir = dependency.GetLocalFilePath();
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (string.IsNullOrEmpty(media.filePath)) return;
                Uri uri = new Uri(media.filePath);
                client.OpenRead(uri);
                var bytes_total = client.ResponseHeaders["Content_Lenght"];
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                client.DownloadFileAsync(uri, $"{dir}/{filename}.pdf");

                ReadFile();
            }
            catch (Exception ex)
            {
                message.LongAlert("Something went wrong please try again");
            }


        }

        private void ReadFile()
        {
            var filename = media.id;
            var all = App.Database.GetDownloadedAudio().Result.FirstOrDefault(x => x.id == filename);
            if(all != null)
            {
                try
                {
                    var dir = dependency.GetLocalFilePath();
                    var fn = $"{dir}/{Id}.pdf";

                    if (Device.RuntimePlatform == Device.Android)
                        Navigation.PushModalAsync(new PdfReaderPage($"file:///android_asset/pdfjs/web/viewer.html?file={WebUtility.UrlEncode(fn)}"));
                }
                catch (Exception ex)
                {


                }
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            var message = DependencyService.Get<Imessaging>();

            var media = App.Database.AddupdatedDownloadedMedia(new DownloadedMedia(this.media)).Result;
            if (media == 1)
            {
                message.LongAlert("Ebook downloaded successfully");
            }
            indicator.IsRunning = false;
            download.IsVisible = true;
            progress.IsVisible = false;

        }

        private void back_Tapped(object sender, EventArgs e)
        {

        }
    }
}