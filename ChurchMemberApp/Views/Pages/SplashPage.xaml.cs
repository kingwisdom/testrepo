using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            sImage.Source = Preferences.Get("churchLogo", string.Empty) ?? "default.png";
            sName.Text = Preferences.Get("churchName", string.Empty) ?? "My Church App";
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            imgLogo.Opacity = 0;
            await Task.WhenAny<bool>
            (
                imgLogo.FadeTo(1, 5000)
            );
            await Task.WhenAny<bool>
            (
                imgLogo.TranslateTo(-DeviceDisplay.MainDisplayInfo.Width, 0, 500)
            );
            //App.Current.MainPage = new MainShellPage();
            if (string.IsNullOrEmpty(Preferences.Get("tenantId", string.Empty)))
            {
                App.Current.MainPage = new NavigationPage(new OnboardPage());
               
            }
            else
            {
                App.Current.MainPage = new MainShellPage();
                //App.Current.MainPage = new test();
            }
        }

        
    }

}