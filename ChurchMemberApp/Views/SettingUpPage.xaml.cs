using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingUpPage : ContentPage
    {
        ChurchProfile Profile { set; get; }
        public SettingUpPage()
        {
            InitializeComponent();
            MessagingCenter.Send<SettingUpPage>(this, "loadFeed");
            Profile = new ChurchProfile();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                btn.IsVisible = false;
                loading.IsVisible = true;
                var res = await ApiServices.GetChurchProfile(App.AppKey);
                if (res)
                {
                    var result = Preferences.Get("churchprofile", string.Empty);
                    if (result != string.Empty)
                    {
                        Profile = JsonConvert.DeserializeObject<ChurchProfile>(result);
                    }
                }
                loading.IsVisible = false;
            }
            catch 
            {
                loading.IsVisible = false;
                btn.IsVisible = true;
            }

            App.Current.MainPage = new MainShellPage();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(2000);
            delay.IsVisible = false;

        }
    }
}