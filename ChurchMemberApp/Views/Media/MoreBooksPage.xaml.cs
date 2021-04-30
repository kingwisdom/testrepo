using ChurchMemberApp.Models.Response;
using ChurchMemberApp.ViewModel.Media;
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
    public partial class MoreBooksPage : ContentPage
    {
        public MoreBooksPage()
        {
            InitializeComponent();
            BindingContext = new MediaViewModel();
        }

        private async void bookfeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = new BookDetailsPage(e.CurrentSelection[0] as ChurchMedia);
            await Navigation.PushAsync(item);
        }
    }
}