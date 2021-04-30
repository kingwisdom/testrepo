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
    public partial class PasswordOTPPage : ContentPage
    {
        string Email;
        public PasswordOTPPage(string email)
        {
            InitializeComponent();
            Email = email;
        }

        private async void otp_Clicked(object sender, EventArgs e)
        {
            //var mail = Preferences.Get("userEmailToChangePW", string.Empty);
            otpButton.IsVisible = false;
            loading.IsVisible = true;
            // PasswordOTPResponse response = new PasswordOTPResponse();
            try
            {
                var response = await ApiServices.VerifyOTP(otp.Text, Email);
                if (response.status)
                {
                    MessageDialog.Show("", response.response, "Ok");

                    var token = response.returnObject.resettoken;
                    await Navigation.PushModalAsync(new ChangePassword(token));
                }
                else
                {
                    MessageDialog.Show("", response.response, "Cancel");
                }
                otpButton.IsVisible = true;
                loading.IsVisible = false;
            }
            catch (Exception)
            {
                otpButton.IsVisible = true;
                loading.IsVisible = false;
            }


        }

        //private async void close_Tapped(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync("../..");
    


    }
}