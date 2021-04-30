using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Media;
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
    public partial class MoreAudioPage : ContentPage
    {
        public MoreAudioPage()
        {
            InitializeComponent();
            BindingContext = new MediaViewModel();
        }


        private async void audiofeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.CurrentSelection[0] as ChurchMedia;
            if (item == null)
                return;


            await Navigation.PushModalAsync(new PlayAudioPage(item, "dd"));
            MessagingCenter.Send(this, "Stream", item);
            
        }
    }
}