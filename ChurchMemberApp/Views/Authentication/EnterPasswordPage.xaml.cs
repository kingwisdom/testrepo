using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
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
    public partial class EnterPasswordPage : ContentPage
    {
        string OTP;
        public EnterPasswordPage(string otp)
        {
            InitializeComponent();
            OTP = otp;
        }

        private async void send_Clicked(object sender, EventArgs e)
        {
            if(password.Text != cpassword.Text)
            {
                MessageDialog.Show("","Your password and confirm password does not match", "Ok");
                return;
            }
            if(confirmOtp.Text != OTP)
            {
                error.IsVisible = true;
                return;
            }
            var request = new SyncDataResponse()
            {
                password = password.Text,
                userID = Preferences.Get("userIdFromDb", string.Empty),
                tenantID = App.AppKey
            };

           var response = await ApiServices.SyncData(request);
            error.IsVisible = false;
            if (response.status)
            {
                MessageDialog.Show("", response.response, "Ok");
            }

            await Navigation.PopToRootAsync();

        }
    }
}