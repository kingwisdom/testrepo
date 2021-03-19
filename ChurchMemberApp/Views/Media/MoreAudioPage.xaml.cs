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
            var item = new MediaDetailPage(e.CurrentSelection[0] as ChurchMedia);
            await Navigation.PushAsync(item);
        }
    }
}