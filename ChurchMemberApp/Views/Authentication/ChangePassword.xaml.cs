using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Services;
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
    public partial class ChangePassword : ContentPage
    {
        string ptoken;
        public ChangePassword(string t)
        {
            InitializeComponent();
            ptoken = t;
        }

        //private async void close_Tapped(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync("../..");
    

        private async void change_Clicked(object sender, EventArgs e)
        {
            var pass = password.Text;
            var cpass = cpassword.Text;

            try
            {

                if (pass != cpass)
                {
                    MessageDialog.Show("", "Password and confirm passwor is not match", "Ok");
                }

                ResetPassword req = new ResetPassword()
                {
                    email = Preferences.Get("userEmailToChangePW", string.Empty),
                    password = pass,
                    resetToken = ptoken
                };
                var response = await ApiServices.ChangePassword(req);

                if (response.status)
                {
                    MessageDialog.Show("", response.response, "Ok");
                    App.Current.MainPage = new MainShellPage();
                    //await Navigation.PopToRootAsync();
                }
                else
                {
                    MessageDialog.Show("", response.response, "Cancel");
                }
            }
            catch (Exception r)
            {
                MessageDialog.Show("", r.Message, "Cancel");
                App.Current.MainPage = new MainShellPage();
            }

        }
    }
}