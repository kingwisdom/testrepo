using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Giving;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Media;
using ChurchMemberApp.Views.Pages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Giving
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPaymentMethodPage : ContentPage
    {
        ContributionItemVM vm;
        public AddPaymentMethodPage()
        {
            InitializeComponent();
        }
        public AddPaymentMethodPage(PaymentForm model)
        {
            InitializeComponent();
            BindingContext = vm = new ContributionItemVM(model);
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            med.Opacity = 0;
            await med.FadeTo(1, 600);
           
        }


        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private async void back_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("../..");
        }

       
    }
}
