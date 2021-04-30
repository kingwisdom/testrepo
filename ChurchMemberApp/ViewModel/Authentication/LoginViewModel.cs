using System;
using System.Windows.Input;
using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Pages;
using ChurchMemberApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using UnixTimeStamp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Authentication
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            IsNotBusy = true;
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        [Obsolete]
        public ICommand LoginCommand => new Command(async() =>
        {
            Preferences.Set("email", Email);
            Preferences.Set("password", Password);

            var login = new Login
            {
                email = Email.Trim(),
                password = Password.Trim()
            };

            try
            {
                ApiServices services = new ApiServices();
                await PopupNavigation.PushAsync(new Indicator());
                var response = await ApiServices.Login(login);
                if(!response.status)
                {
                    MessageDialog.Show("", response.response, "Cancel");
                    await PopupNavigation.PopAsync();
                    return;
                }
                var r = response.returnObject.expiryTime;
                long longTime = r.Ticks;

                Preferences.Set("token", response.returnObject.token);
                Preferences.Set("userId", response.returnObject.userId);
                Preferences.Set("userName", response.returnObject.email);
                Preferences.Set("fullName", response.returnObject.fullname);
                Preferences.Set("tokenExpirationTime", longTime);
                Preferences.Set("currentTime", UnixTime.GetCurrentTime());

                await ApiServices.GetAllPostCategory();

                await services.GetAllChurchFeeds(App.AppKey, Preferences.Get("userId", string.Empty));
                await PopupNavigation.PopAsync();
                await App.Current.MainPage.Navigation.PopModalAsync();// = new FeedPage();
            }
            catch (Exception er)
            {
                await PopupNavigation.PopAsync();
                MessageDialog.Show("Error!", "Invalid email or password", "Cancel");
            }
            
        });
        [Obsolete]
        public ICommand RegisterCommand => new Command(async() =>
        {
            IsBusy = true;
            IsNotBusy = false;
            
            //if (string.IsNullOrEmpty(Email) && string.IsNullOrWhiteSpace(Phone) && Phone.Length <11)
            //{
            //    MessageDialog.Show("Error!", "Please fill your Email and Phone Number and continue", "Ok");
            //    IsBusy = false;
            //    return;
            //}
            var register = new Register
            {
                name = Name,
                tenantID = App.AppKey,
                email = Preferences.Get("newEmail",string.Empty).Trim(),
                password = Password.Trim(),
                phoneNumber = Preferences.Get("newPhone", string.Empty)
            };

            try
            {
                //await PopupNavigation.PushAsync(new Indicator());
                var response = await ApiServices.Register(register);
                if (response.status)
                {
                    IsBusy = false;
                    //await PopupNavigation.PopAsync();
                    MessageDialog.Show("Success", response.response, "Ok");
                    await App.Current.MainPage.Navigation.PopModalAsync();// = new FeedPage();
                }
                else
                {
                    //await PopupNavigation.PopAsync();
                    IsBusy = false;
                    MessageDialog.Show("", response.response, "Cancel");
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            catch (Exception er)
            {
                IsBusy = false;
                await App.Current.MainPage.Navigation.PopModalAsync();
                //MessageDialog.Show("Error!", er.Message, "Cancel");
            }

            IsNotBusy = true;
        });
        
    }
}
