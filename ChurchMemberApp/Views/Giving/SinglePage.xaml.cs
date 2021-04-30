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
    public partial class SinglePage : ContentPage
    {
        public SinglePage()
        {
            InitializeComponent();
        }

        //private async void oft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var item = new ContributionItemPage(e.CurrentSelection[0] as PaymentForm);
        //    await Navigation.PushAsync(item);
        //}
    }
}