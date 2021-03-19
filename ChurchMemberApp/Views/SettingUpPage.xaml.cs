using ChurchMemberApp.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingUpPage : ContentPage
    {
        public SettingUpPage()
        {
            InitializeComponent();
            MessagingCenter.Send<SettingUpPage>(this, "loadFeed");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
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