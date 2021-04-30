using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Giving;
using ChurchMemberApp.Views.Giving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Media
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LiveGivePage : ContentPage
    {
        OfferingFormViewModel vm;
        public LiveGivePage()
        {
            InitializeComponent();
            BindingContext = vm = new OfferingFormViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.GetPaymentForms.Execute(null);
        }
        private async void oft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = new ContributionItemPage(e.CurrentSelection[0] as PaymentForm);
            await Navigation.PushAsync(item);
        }

        private async void back_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}