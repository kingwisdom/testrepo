using ChurchMemberApp.Models.Request;
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
    public partial class InitialSignupPage : ContentPage
    {
        public InitialSignupPage()
        {
            InitializeComponent();
        }

        private void back_Tapped(object sender, EventArgs e)
        {

        }

        private async void proceed_Clicked(object sender, EventArgs e)
        {
            try
            {
                loading.IsVisible = true;
                if (email.Text == "" || phone.Text == "")
                {
                    error.IsVisible = true;
                    loading.IsVisible = false;
                    return;
                }
                var model = new CheckDataRequest()
                {
                    phoneNumber = phone.Text,
                    email = email.Text,
                    tenantId = App.AppKey
                };
                var res = await ApiServices.CheckData(model);
                if (res != null)
                {
                    if (res.status)
                    {
                        Preferences.Set("userIdFromDb", res.returnObject.record2.personId);
                        var answer = await DisplayAlert("", "Your data exists in our database, Do you want to sync now?", "Yes", "No");
                        if (answer)
                        {
                            var response = await ApiServices.GetOTP(phone.Text, App.AppKey);
                            if (response != null)
                            {
                                await Navigation.PushModalAsync(new EnterPasswordPage(response.returnObject.otp));
                            }
                            else
                            {
                                MessageDialog.Show("", "Network Error, Please check your conneection or restart the app", "Cancel");
                                await Navigation.PopModalAsync();
                            }
                        }
                        //MessageDialog.Show("","Your data exists in our database, Do you want to sync now?","Ok", async() => 
                        //{


                        //});
                        loading.IsVisible = false;

                    }

                }
                else
                {
                    Preferences.Set("newEmail", email.Text);
                    Preferences.Set("newPhone", phone.Text);
                    await Navigation.PushModalAsync(new RegisterPage());
                    loading.IsVisible = false;
                  
                }
                
            }
            catch (Exception x)
            {
                await DisplayAlert("", x.Message, "Cancel");
                loading.IsVisible = false;
            }
        }

        private void login_Tapped(object sender, EventArgs e)
        {

        }
    }
}