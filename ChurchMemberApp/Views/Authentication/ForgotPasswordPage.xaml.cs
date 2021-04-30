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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        //private async void back_Tapped(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync("../..");
        //}

        private async void login_Clicked(object sender, EventArgs e)
        {
            if(email.Text == "")
            {
                MessageDialog.Show("", "Please enter a valid email", "Ok");
                return;
            }
            try
            {
                btn.IsEnabled = false;
                loading.IsVisible = true;
                var result = await ApiServices.ForgotPAssword(email.Text);
               // Preferences.Set("userEmailToChangePW", email.Text);
                if (result.status)
                    await Navigation.PushModalAsync(new PasswordOTPPage(email.Text));
                else
                    MessageDialog.Show("", result.response, "Ok");
            }
            catch (Exception mx)
            {
                MessageDialog.Show("", mx.Message, "Cancel");
            }

            btn.IsEnabled = true;
            loading.IsVisible = false;
        }

        //private async void close_Tapped(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync("../..");
        //}
    }
}