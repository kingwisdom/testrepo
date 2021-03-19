using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Media;
using ChurchMemberApp.Views.Pages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Giving
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
        public PaymentPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            med.Opacity = 0;
            await med.FadeTo(1, 1000);
            active.Opacity = 0;
            await Task.WhenAny<bool>
            (
                active.FadeTo(1, 2000),
                active.TranslateTo(0, 0, 2000)
            );
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new GiveTowardsPage());
        }

        private void home_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new FeedPage());
        }

        private void media_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MediaPage());
        }

        private void profile_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new ProfilePage());
        }

        private void pay_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaymentPage());
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
