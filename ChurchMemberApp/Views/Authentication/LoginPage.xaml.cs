using System;
using System.Collections.Generic;
using ChurchMemberApp.ViewModel.Authentication;
using ChurchMemberApp.Views.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        void register_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new InitialSignupPage());
        }

        private async void close_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("../..");
        }

        private void forgot_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ForgotPasswordPage());
        }
    }
}
