using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Giving;
using ChurchMemberApp.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Media
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YoutubeLive : ContentPage
    {
        public YoutubeLive()
        {
            InitializeComponent();
            webView.Source = "https://www.youtube.com/channel/UCtK5fHLQo56yH6Ru6Z6GuKg";
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void give_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GivePage());
        }

        private async void mark_Tapped(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                await Navigation.PushAsync(new LoginPage());
                return;
            }

            await Navigation.PushAsync(new AttendanceWithBarcodePage());
        }

    }
}