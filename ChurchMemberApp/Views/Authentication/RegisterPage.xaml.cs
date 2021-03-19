using ChurchMemberApp.ViewModel.Authentication;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        void back_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void login_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
