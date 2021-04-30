using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Giving;
using ChurchMemberApp.Views.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Media
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediaPage : ContentPage
    {
        private string pagefrompush;
        private string pushtitle;
        private string pushdat;
        private string pushdet;
        private string pushothers;
        private string pushimagelink;

        public MediaPage()
        {
            InitializeComponent();
        }

        public MediaPage(string pagefrompush, string pushtitle, string pushdat, string pushdet, string pushothers, string pushimagelink)
        {
            this.pagefrompush = pagefrompush;
            this.pushtitle = pushtitle;
            this.pushdat = pushdat;
            this.pushdet = pushdet;
            this.pushothers = pushothers;
            this.pushimagelink = pushimagelink;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            med.Opacity = 0;
            await med.FadeTo(1, 100);
            
        }

        //void home_Tapped(System.Object sender, System.EventArgs e)
        //{
        //    App.Current.MainPage = new NavigationPage(new FeedPage());
        //}

        //private void give_Tapped(object sender, EventArgs e)
        //{
        //    App.Current.MainPage = new NavigationPage(new GivePage());
        //}
        

       
    }
}
