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
                email = Email,
                password = Password
            };

            try
            {
                ApiServices services = new ApiServices();
                await PopupNavigation.PushAsync(new Indicator());
                var response = await ApiServices.Login(login);
                if(response == null)
                {
                    MessageDialog.Show("Error!", "Problem in loging in, please check your details and try again", "Cancel");
                    return;
                }
                var r = response.expiryTime;
                long longTime = r.Ticks;

                Preferences.Set("token", response.token);
                Preferences.Set("userId", response.userId);
                Preferences.Set("userName", response.username);
                Preferences.Set("fullName", response.fullname);
                Preferences.Set("tokenExpirationTime", longTime);
                Preferences.Set("currentTime", UnixTime.GetCurrentTime());

                await services.GetAllChurchFeeds(App.AppKey, Preferences.Get("userId", string.Empty));
                await PopupNavigation.PopAsync();
                await App.Current.MainPage.Navigation.PopModalAsync();// = new FeedPage();
            }
            catch (Exception er)
            {
                await PopupNavigation.PopAsync();
                MessageDialog.Show("Error!", er.Message, "Cancel");
            }
            
        });
        [Obsolete]
        public ICommand RegisterCommand => new Command(async() =>
        {
            var register = new Register
            {
                name = Name,
                tenantID = App.AppKey,
                email = Email,
                password = Password,
                phoneNumber = Phone
            };

            try
            {
                await PopupNavigation.PushAsync(new Indicator());
                var response = await ApiServices.Register(register);
                if (response != null)
                {
                    await PopupNavigation.PopAsync();
                    await App.Current.MainPage.Navigation.PopAsync();// = new FeedPage();
                }
                else
                {
                    await PopupNavigation.PopAsync();
                    MessageDialog.Show("Error!", "Error occured from the service please try again later", "Cancel");
                }
            }
            catch (Exception er)
            {
                await PopupNavigation.PopAsync();
                MessageDialog.Show("Error!", er.Message, "Cancel");
            }
            
        });
        
    }
}
