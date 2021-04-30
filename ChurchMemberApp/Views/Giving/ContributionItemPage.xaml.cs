using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Giving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Giving
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContributionItemPage : ContentPage
    {
        ContributionItemVM vm;
        public ContributionItemPage()
        {
            InitializeComponent();
        }
        
        public ContributionItemPage(PaymentForm model)
        {
            InitializeComponent();
            BindingContext = vm = new ContributionItemVM(model);
        }

        private async void back_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("../..");
        }
    }
}