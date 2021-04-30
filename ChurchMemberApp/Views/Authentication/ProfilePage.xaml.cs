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

        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

      
        private async void check_Tapped(object sender, EventArgs e)
        {
            const int _animationTime = 100;
            try
            {
                var layout = (StackLayout)sender;
                await layout.FadeTo(0.5, _animationTime);
                await layout.FadeTo(1, _animationTime);

                if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
                {
                    await Navigation.PushAsync(new LoginPage());
                    return;
                }

                await Navigation.PushAsync(new AttendanceWithBarcodePage());
                
            }
            catch (Exception ex)
            {
            }
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

             App.Current.MainPage = new MainShellPage();
            //await Shell.Current.GoToAsync("../..");
        }

        private void downloads_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AudioList());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                userEmail.Text = Preferences.Get("userName", string.Empty);
                userName.Text = Preferences.Get("fullName", string.Empty);
                logoutBtn.IsVisible = true;
            }
            else
            {
                userEmail.Text = "";
                userName.Text = "";
            }
            med.Opacity = 0;
            await med.FadeTo(1, 500);
        }

        private async void profile_Tapped(object sender, EventArgs e)
        {
            const int _animationTime = 100;
            try
            {
                var layout = (StackLayout)sender;
                await layout.FadeTo(0.5, _animationTime);
                await layout.FadeTo(1, _animationTime);

                if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
                {
                   await Navigation.PushAsync(new LoginPage());
                    return;
                }

                await Navigation.PushAsync(new EditProfilePage());
            }
            catch (Exception ex)
            {

            }
        }


    }
}