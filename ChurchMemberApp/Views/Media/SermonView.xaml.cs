using System;
using System.Collections.Generic;
using System.Linq;
using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using ChurchMemberApp.ViewModel.Media;
using ChurchMemberApp.Views.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Media
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SermonView : ContentView
    {
        MediaViewModel vm;
        public SermonView()
        {
            InitializeComponent();
            BindingContext = vm = new MediaViewModel();
        }


        //private async void allfeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var item = e.CurrentSelection[0] as ChurchMedia;
        //    if (item == null)
        //        return;
            
            
        //    await Navigation.PushModalAsync(new PlayAudioPage(item, "dd"));
        //    MessagingCenter.Send(this, "Stream", item);
        //    // ((CollectionView)sender).SelectedItem = null;
        //}

        //private async void videofeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var item = e.CurrentSelection[0] as ChurchMedia;
        //    if (item == null)
        //        return;
        //    await Navigation.PushModalAsync(new VLCPage(item));
        //}

        private async void current_Tapped(object sender, EventArgs e)
        {
            var item = vm.CurrentMessage;
            if(item.mediaType == MediaType.Audio)
            {
                await Navigation.PushModalAsync(new PlayAudioPage(item, "dd"));
                MessagingCenter.Send(this, "Stream", item);
            }
            else if (item.mediaType == MediaType.Video)
            {
                await Navigation.PushModalAsync(new VLCPage(item));
            }
            else if (item.mediaType == MediaType.Ebook)
            {
                ///await Navigation.PushModalAsync(new PdfReaderPage(item));
            }
            else if (item.mediaType == MediaType.Picture)
            {
                await Navigation.PushModalAsync(new Picturepopup(item));
            }
            else
            {
                return;
            }
        }
        private async void books_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.CurrentSelection[0] as ChurchMedia;
            if (item == null)
                return;


            var filename = item.name;
            var all = App.Database.GetDownloadedAudio().Result.FirstOrDefault(x => x.name == filename);
            if(all!= null)
            {
                await Navigation.PushModalAsync(new PdfReaderPage(all));
            }

            await Navigation.PushModalAsync(new BookDetailsPage(item));
            //await Navigation.PushModalAsync(new PdfReaderPage(item));
        }

        
    }
}
