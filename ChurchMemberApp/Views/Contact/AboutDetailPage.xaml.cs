using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Contact;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Contact
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutDetailPage : ContentPage
    {
        AboutDetailViewModel vm;
        public AboutDetailPage()
        {
            InitializeComponent();

            BindingContext = new AboutDetailViewModel();
            //var res = Preferences.Get("churchprofile", string.Empty);
        }

        //public AboutDetailPage(About model)
        //{
        //    InitializeComponent();
        //    vm = new AboutDetailViewModel(model);
        //    this.BindingContext = model;
        //}

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}