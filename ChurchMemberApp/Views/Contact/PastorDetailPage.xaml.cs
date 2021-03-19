using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Contact
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PastorDetailPage : ContentPage
    {
        public PastorDetailPage()
        {
            InitializeComponent();
        }
        
        public PastorDetailPage(Pastor pastor)
        {
            InitializeComponent();
            BindingContext = new PastorDetailVM(pastor);
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var soc = e.CurrentSelection[0] as SocialMedia;

            try
            {
                await Browser.OpenAsync(soc.url, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
               //await DisplayAlert("", ex.Message, "Cancel");
            }
        }
    }
}