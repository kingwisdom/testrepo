using System;
using ChurchMemberApp.Models;
using ChurchMemberApp.ViewModel.Pages;
using ChurchMemberApp.Views.Authentication;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedPage : ContentPage
    {
        FeedViewModel vm;
        public FeedPage()
        {
            InitializeComponent();
            BindingContext = vm = new FeedViewModel();
            //SetLayoutFrame();

            var minutes = TimeSpan.FromSeconds(60);

            Device.StartTimer(minutes, () => {
                vm.GetFeedsCommand.Execute(null);
                return true;
            });
            
        }

      
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //active.Opacity = 0;
            //await Task.WhenAny<bool>
            //(
            //  active.FadeTo(1, 2000),
            //   active.TranslateTo(0, 0, 2000)
            //);
        }

       
        
        private bool _canClose = true;

        protected override bool OnBackButtonPressed()
        {
            if (_canClose)
            {
                ShowExitDialog();
            }
            return false; 
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            if (answer)
            {
                _canClose = false;
                OnBackButtonPressed();
            }
        }

        private async void TransCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sFeed = new FeedDetailPage(e.CurrentSelection[0] as Feeds);
            await Navigation.PushAsync(sFeed);
        }

        private void UIChange_Tapped(object sender, EventArgs e)
         {
           // App.Current.UserAppTheme = (App.Current.UserAppTheme == OSAppTheme.Light) ? OSAppTheme.Light : OSAppTheme.Dark;
        }

        private async void createPost_Tapped(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                await Navigation.PushModalAsync(new LoginPage());

                return;
            }
            await Navigation.PushModalAsync(new CreatePostPage());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var minutes = TimeSpan.FromMinutes(-1);

            Device.StartTimer(minutes, () => {
                vm.GetFeedsCommand.Execute(null);
                return true;
            });
        }
    }
}
