using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Giving;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Media;
using ChurchMemberApp.Views.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Giving
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GivePage : ContentPage
    {
        OfferingFormViewModel vm;
        public GivePage()
        {
            InitializeComponent();
            BindingContext = vm = new OfferingFormViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            vm.GetPaymentForms.Execute(null);
            med.Opacity = 0;
            await med.FadeTo(1, 1000);
            //active.Opacity = 0;
            //await Task.WhenAny<bool>
            //(
            //    active.FadeTo(1, 2000),
            //    active.TranslateTo(0, 0, 2000)
            //);
        }

      
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private async void oft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = new ContributionItemPage(e.CurrentSelection[0] as PaymentForm);
            await Navigation.PushAsync(item);
        }
    }
}
