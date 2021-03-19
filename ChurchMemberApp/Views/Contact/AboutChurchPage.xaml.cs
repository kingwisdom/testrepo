using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Contact
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutChurchPage : ContentPage
    {
        ChurchContactViewModel vm;
        public AboutChurchPage()
        {
            InitializeComponent();
            this.BindingContext=vm = new ChurchContactViewModel();

        }

   

        //private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var sFeed = new AboutDetailPage(e.CurrentSelection[0] as About);
        //    await Navigation.PushAsync(sFeed);
        //}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void about_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutDetailPage());
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sFeed = new PastorDetailPage(e.CurrentSelection[0] as Pastor);
            await Navigation.PushAsync(sFeed);
        }
    }
}