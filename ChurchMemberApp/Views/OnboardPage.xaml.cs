using System;
using System.Collections.Generic;
using ChurchMemberApp.Views.Authentication;
using Xamarin.Forms;

namespace ChurchMemberApp.Views
{
    public partial class OnboardPage : ContentPage
    {
        public OnboardPage()
        {
            InitializeComponent();
        }

        void start_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new SelectChurchPage());
        }

    }
}
