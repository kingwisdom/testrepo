using ChurchMemberApp.Views.Contact;
using ChurchMemberApp.Views.Giving;
using ChurchMemberApp.Views.Media;
using ChurchMemberApp.Views.Media.Downloads.Audio;
using ChurchMemberApp.Views.Pages;
using ChurchMemberApp.Views.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            // SetLayoutFrame();

            userEmail.Text = Preferences.Get("userName", string.Empty);
            userName.Text = Preferences.Get("fullName", string.Empty);
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        //private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        if (sender is Frame frmHome && frmHome.StyleId.Equals("frameHome"))
        //        {
        //            gridFrames.ColumnDefinitions[0].Width = GridLength.Star;
        //            gridFrames.ColumnDefinitions[1].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[2].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[3].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[4].Width = GridLength.Auto;
        //            frmHome.Padding = new Thickness(22, 5);
        //            frmHome.BackgroundColor = Color.FromHex("#102733");
        //            lbHome.IsVisible = true;
        //            lbHome.TextColor = Color.FromHex("#FFA700");
        //            lbIconHome.TextColor = Color.FromHex("#FFA700");
        //            frameSearch.Padding = new Thickness(0);
        //            frameSearch.BackgroundColor = Color.Transparent;
        //            lbSearch.IsVisible = false;
        //            lbSearch.TextColor = Color.White;
        //            lbIconSearch.TextColor = Color.White;
        //            frameFavorite.Padding = new Thickness(0);
        //            frameFavorite.BackgroundColor = Color.Transparent;
        //            lbFavorite.IsVisible = false;
        //            lbFavorite.TextColor = Color.White;
        //            lbGroup.IsVisible = false;
        //            lbGroup.TextColor = Color.Transparent;
        //            lbMore.IsVisible = false;
        //            lbMore.TextColor = Color.Transparent;
        //            lbIconFavorite.TextColor = Color.White;
        //            App.Current.MainPage = new NavigationPage(new FeedPage());
        //        }
        //        else if (sender is Frame frmSearch && frmSearch.StyleId.Equals("frameSearch"))
        //        {
        //            gridFrames.ColumnDefinitions[0].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[1].Width = GridLength.Star;
        //            gridFrames.ColumnDefinitions[2].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[3].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[4].Width = GridLength.Auto;
        //            frameHome.Padding = new Thickness(0);
        //            frameHome.BackgroundColor = Color.Transparent;
        //            lbHome.IsVisible = false;
        //            lbHome.TextColor = Color.White;
        //            lbIconHome.TextColor = Color.White;
        //            frmSearch.Padding = new Thickness(22, 5);
        //            frmSearch.BackgroundColor = Color.FromHex("#102733");
        //            lbSearch.IsVisible = true;
        //            lbSearch.TextColor = Color.FromHex("#FFA700");
        //            lbIconSearch.TextColor = Color.FromHex("#FFA700");
        //            frameFavorite.Padding = new Thickness(0);
        //            frameFavorite.BackgroundColor = Color.Transparent;
        //            lbFavorite.IsVisible = false;
        //            lbGroup.IsVisible = false;
        //            lbGroup.TextColor = Color.Transparent;
        //            lbMore.IsVisible = false;
        //            lbMore.TextColor = Color.Transparent;
        //            lbFavorite.TextColor = Color.White;
        //            lbIconFavorite.TextColor = Color.White;

        //            App.Current.MainPage = new NavigationPage(new MediaPage());
        //        }
        //        else if (sender is Frame frmFavorite && frmFavorite.StyleId.Equals("frameFavorite"))
        //        {
        //            gridFrames.ColumnDefinitions[0].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[1].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[2].Width = GridLength.Star;
        //            gridFrames.ColumnDefinitions[3].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[4].Width = GridLength.Auto;
        //            frameHome.Padding = new Thickness(0);
        //            frameHome.BackgroundColor = Color.Transparent;
        //            lbHome.IsVisible = false;
        //            lbHome.TextColor = Color.White;
        //            lbIconHome.TextColor = Color.White;
        //            frameSearch.Padding = new Thickness(0);
        //            frameSearch.BackgroundColor = Color.Transparent;
        //            lbSearch.IsVisible = false;
        //            lbSearch.TextColor = Color.White;
        //            lbIconSearch.TextColor = Color.White;
        //            frmFavorite.Padding = new Thickness(22, 5);
        //            frmFavorite.BackgroundColor = Color.FromHex("#102733");
        //            lbFavorite.IsVisible = true;
        //            lbFavorite.TextColor = Color.FromHex("#FFA700");
        //            lbGroup.IsVisible = false;
        //            lbGroup.TextColor = Color.Transparent;
        //            lbMore.IsVisible = false;
        //            lbMore.TextColor = Color.Transparent;
        //            lbIconFavorite.TextColor = Color.FromHex("#FFA700");
        //            App.Current.MainPage = new NavigationPage(new GivePage());
        //        }
        //        else if (sender is Frame frmGroup && frmGroup.StyleId.Equals("frameGroup"))
        //        {
        //            gridFrames.ColumnDefinitions[0].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[1].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[2].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[3].Width = GridLength.Star;
        //            gridFrames.ColumnDefinitions[4].Width = GridLength.Auto;
        //            frameHome.Padding = new Thickness(0);
        //            frameHome.BackgroundColor = Color.Transparent;
        //            lbHome.IsVisible = false;
        //            lbHome.TextColor = Color.White;
        //            lbIconHome.TextColor = Color.White;
        //            frameSearch.Padding = new Thickness(0);
        //            frameSearch.BackgroundColor = Color.Transparent;
        //            lbSearch.IsVisible = false;
        //            lbSearch.TextColor = Color.White;
        //            lbIconSearch.TextColor = Color.White;
        //            frmGroup.Padding = new Thickness(22, 5);
        //            frmGroup.BackgroundColor = Color.Transparent;
        //            lbFavorite.IsVisible = false;
        //            lbFavorite.TextColor = Color.Transparent;
        //            lbGroup.IsVisible = true;
        //            lbGroup.TextColor = Color.FromHex("#FFA700");
        //            lbMore.IsVisible = false;
        //            lbMore.TextColor = Color.Transparent;
        //            lbIconGroup.TextColor = Color.FromHex("#FFA700");
        //            App.Current.MainPage = new NavigationPage(new GroupChatPage());
        //        }

        //        else if (sender is Frame frmMore && frmMore.StyleId.Equals("MoreGroup"))
        //        {
        //            gridFrames.ColumnDefinitions[0].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[1].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[2].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[3].Width = GridLength.Auto;
        //            gridFrames.ColumnDefinitions[4].Width = GridLength.Star;
        //            frameHome.Padding = new Thickness(0);
        //            frameHome.BackgroundColor = Color.Transparent;
        //            lbHome.IsVisible = false;
        //            lbHome.TextColor = Color.White;
        //            lbIconHome.TextColor = Color.White;
        //            frameSearch.Padding = new Thickness(0);
        //            frameSearch.BackgroundColor = Color.Transparent;
        //            lbSearch.IsVisible = false;
        //            lbSearch.TextColor = Color.White;
        //            lbIconSearch.TextColor = Color.White;
        //            frmMore.Padding = new Thickness(22, 5);
        //            frmMore.BackgroundColor = Color.Transparent;
        //            lbFavorite.IsVisible = false;
        //            lbFavorite.TextColor = Color.Transparent;
        //            lbGroup.IsVisible = false;
        //            lbGroup.TextColor = Color.Transparent;
        //            lbMore.IsVisible = true;
        //            lbMore.TextColor = Color.FromHex("#FFA700");
        //            lbIconMore.TextColor = Color.FromHex("#FFA700");
        //            App.Current.MainPage = new NavigationPage(new MorePage());
        //        }
        //    });
        //}

        //private void SetLayoutFrame()
        //{
        //    gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        //    gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        //    gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
        //    gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        //    gridFrames.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        //    frameHome.Padding = new Thickness(0);
        //    frameHome.BackgroundColor = Color.Transparent;
        //    lbHome.IsVisible = false;

        //    frameSearch.Padding = new Thickness(0);
        //    frameSearch.BackgroundColor = Color.Transparent;
        //    lbSearch.IsVisible = false;
        //    lbSearch.TextColor = Color.White;
        //    lbIconSearch.TextColor = Color.White;

        //    frameFavorite.Padding = new Thickness(0);
        //    frameFavorite.BackgroundColor = Color.Transparent;
        //    lbFavorite.IsVisible = false;
        //    lbFavorite.TextColor = Color.White;
        //    lbIconFavorite.TextColor = Color.White;

        //    frameGroup.Padding = new Thickness(0);
        //    frameGroup.BackgroundColor = Color.Transparent;
        //    lbGroup.IsVisible = false;
        //    lbGroup.TextColor = Color.White;
        //    lbIconGroup.TextColor = Color.White;

        //    frameMore.Padding = new Thickness(22, 5);
        //    frameMore.BackgroundColor = Color.FromHex("#102733");
        //    lbMore.IsVisible = true;
        //    lbMore.TextColor = Color.White;
        //    lbIconMore.TextColor = Color.White;
        //}

        private void check_Tapped(object sender, EventArgs e)
        {
            var s = sender as StackLayout;
            s.BackgroundColor = Color.Orange;
            Navigation.PushAsync(new AttendanceWithBarcodePage());

            s.BackgroundColor = Color.Transparent;
        }

        private void cinfo_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutChurchPage());
        }

        private void logout_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("token", string.Empty);
            Preferences.Set("userId", string.Empty);
            Preferences.Set("userName", string.Empty);

            MessageDialog.Show("Success", "You Logged Out Successfully", "Ok");

            //App.Current.MainPage = new NavigationPage(new FeedPage());
        }

        private void downloads_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AudioList());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            med.Opacity = 0;
            await med.FadeTo(1, 500);
        }
    }
}