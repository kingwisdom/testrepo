using System;
using System.Collections.Generic;
using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Media;
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


        private async void allfeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = new MediaDetailPage(e.CurrentSelection[0] as ChurchMedia);
            await Navigation.PushModalAsync(item);
        }

        private void videofeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void current_Tapped(object sender, EventArgs e)
        {
            var item = new MediaDetailPage(vm.CurrentMessage);
            await Navigation.PushAsync(item);
        }
    }
}
