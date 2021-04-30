using ChurchMemberApp.DB;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using ChurchMemberApp.ViewModel.Media;
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
    public partial class MediaDetailPage : ContentPage
    {
        public long bytes_total { get; set; }
        public long Size { get; set; }
        IFileHelper dependency { get; set; }
        Imessaging message { get; set; }
        MediaDetailVM vm;
        public MediaDetailPage()
        {
            InitializeComponent();
        }

        ChurchMedia media;
        public MediaDetailPage(ChurchMedia model)
        {
            InitializeComponent();
            this.BindingContext = new MediaDetailVM(model);
            media = model;
            
        }

        

        private async void back_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void play_Tapped(object sender, EventArgs e)
        {
            if(media.mediaType == MediaType.Video)
            {
                await Navigation.PushModalAsync(new VLCPage(media));
                return;
            }
            await Navigation.PushModalAsync(new PlayAudioPage(media, "dd"));
                MessagingCenter.Send(this, "Stream", media);
            // var isusersignedin = Preferences.Get(Preferences.Get(), false);
            //var hasactivesubscription = Preferences.Get(App.HASACTIVESUBSCRIPTION, false);
            //if (media.IsFree)
            //{

            //    if (string.IsNullOrEmpty(media.FilePath))
            //    {
            //        return;
            //    }
            //    await Navigation.PushAsync(new AudioPlayer(media, "dd"));
            //    MessagingCenter.Send(this, "Stream", media);
            //}
            //else
            //{
            //    if (isusersignedin)
            //    {
            //        if (hasactivesubscription)
            //        {

            //            if (string.IsNullOrEmpty(media.FilePath))
            //            {
            //                return;
            //            }
            //            await Navigation.PushAsync(new AudioPlayer(media, "dd"));
            //            MessagingCenter.Send(this, "Stream", media);
            //        }
            //        else
            //        {
            //            await Navigation.PushPopupAsync(new ActiveSubDialog());
            //        }

            //    }
            //    else
            //    {
            //        await Navigation.PushPopupAsync(new AuthenticationDialog());
            //    }
            //}
            //((CollectionView)sender).SelectedItem = null;
        }

        private void download_Clicked(object sender, EventArgs e)
        {
            //var isusersignedin = Preferences.Get(App.USERSIGNEDINKEY, string.Empty);
            //var hasactivesubscription = Preferences.Get(App.HASACTIVESUBSCRIPTION, false);
            downloadfile();
            //if (media.IsFree)
            //{

            //    if (string.IsNullOrEmpty(media.FilePath))
            //    {
            //        return;
            //    }
            //    downloadfile();
            //}
            //else
            //{
            //    if (isusersignedin)
            //    {
            //        if (hasactivesubscription)
            //        {
            //            char x = (char)8358;
            //            var answer = await DisplayAlert("Download", $"You will be charged {x}{media.Price} for downloading this file", "Continue", "Cancel");
            //            if (answer)
            //            {
            //                if (await DeductUser())
            //                {
            //                    if (string.IsNullOrEmpty(media.FilePath))
            //                    {
            //                        return;
            //                    }
            //                    downloadfile();
            //                }
            //            }
            //            else
            //            {
            //                return;
            //            }

            //        }
            //        else
            //        {
            //            await Navigation.PushPopupAsync(new ActiveSubDialog());
            //        }

            //    }
            //    else
            //    {
            //        await Navigation.PushPopupAsync(new AuthenticationDialog());
            //    }
            //}
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Text = e.ProgressPercentage.ToString() + "%";
        }
        public void downloadfile()
        {
            indicator.IsRunning = true;
            download.IsEnabled = false;
            message = DependencyService.Get<Imessaging>();
            try
            {
                if (string.IsNullOrEmpty(media.filePath))
                {
                    return;
                }
                indicator.IsRunning = true;
                download.IsEnabled = false;
                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    message.LongAlert("No Internet Connection");
                    return;
                }
                
                progress.IsVisible = true;
                //PlayButton.IsEnabled = false;
                dependency = DependencyService.Get<IFileHelper>();
                message = DependencyService.Get<Imessaging>();
                var filename = media.id;
                
                var all =  App.Database.GetDownloadedAudio().Result.FirstOrDefault(x => x.name == filename);
                if (all != null)
                {
                    message.LongAlert("This Audio has already been downloaded, please check your Downloads");
                    indicator.IsRunning = false;
                    download.IsEnabled = true;
                   // PlayButton.IsEnabled = true;
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


                client.DownloadFileAsync(uri, $"{dir}/{filename}.mp3");
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
            }
            catch (Exception ex)
            {
                message.LongAlert("Something went wrong please try again");
            }


        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            var message = DependencyService.Get<Imessaging>();

            var media = App.Database.AddupdatedDownloadedMedia(new Models.DownloadedMedia(this.media)).Result;
            if (media ==1)
            {
                message.LongAlert("Audio downloaded successfully");
            }
            indicator.IsRunning = false;
            download.IsEnabled = true;
            progress.IsVisible = false;
           // PlayButton.IsEnabled = true;
            // var all =     App.Database.GetDownLoadedAudio();
            //var stream = dependency.GetFileStream("audioone.mp3");
            // CrossMediaManager.Current.Play(stream.Name);

        }

    }
}