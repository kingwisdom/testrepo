using ChurchMemberApp.ViewModel.Pages;
using ChurchMemberApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePostPage : ContentPage
    {
        public CreatePostPage()
        {
            InitializeComponent();
            BindingContext = new CreatePostViewModel();
            username.Text = Preferences.Get("userName", string.Empty);
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new FeedPage();
        }

        [Obsolete]
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new SelectPhoto());
        }
    }
}