using System;
using System.Collections.ObjectModel;
using System.Linq;
using ChurchMemberApp.Models;
using ChurchMemberApp.Platform;
using ChurchMemberApp.ViewModel.Pages;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Media;
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

            var minutes = TimeSpan.FromMinutes(2);

            Device.StartTimer(minutes, () => {
                vm.GetFeedsCommand.Execute(null);
                return true;
            });
            
        }

      
        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.OnAppearing();
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

        //private async void TransCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var nav = e.CurrentSelection[0] as Feeds;
        //    if (nav.type == "Video") { await Navigation.PushModalAsync(new VLCPage(nav.mediaUrl)); }
        //    else
        //    {
        //        var sFeed = new FeedDetailPage(nav);
        //        await Navigation.PushAsync(sFeed);
        //    }

        //    //((CollectionView)sender).SelectedItem = null;
        //}

        private void UIChange_Tapped(object sender, EventArgs e)
         {
           // App.Current.UserAppTheme = (App.Current.UserAppTheme == OSAppTheme.Light) ? OSAppTheme.Light : OSAppTheme.Dark;
        }

        //private async void createPost_Tapped(object sender, EventArgs e)
        //{

        //}
  

        private async void create_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                await Navigation.PushModalAsync(new LoginPage());

                return;
            }
            await Navigation.PushModalAsync(new CreatePostPage());
        }

        private async void give_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LiveGivePage());
        }

        private async void mark_Tapped(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                await Navigation.PushAsync(new LoginPage());
                return;
            }

            await Navigation.PushAsync(new AttendanceWithBarcodePage());
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var message = DependencyService.Get<Imessaging>();
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                message.LongAlert("Internet connection is required to play audio");
                return;
            }
            else
            {
                var item = e.SelectedItem as Feeds;
                if (e.SelectedItem == null)
                {
                    return;
                }
                Navigation.PushAsync(new FeedDetailPage(item));

                FeedViewModel.instance.SearchlistIsvisible = false;
                 listview.SelectedItem = null;
            }

        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                FeedViewModel.instance.SearchlistIsvisible = true;
                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    FeedViewModel.instance.SearchlistIsvisible = false;
                }
                var fulllist = FeedViewModel.instance.Feeds;

                var hh = fulllist.Where(a => a.content == null).FirstOrDefault();

                fulllist.Remove(hh);

                FeedViewModel.instance.SearhList = new ObservableCollection<Feeds>(fulllist.Where(a => a.content.ToLower().Contains(e.NewTextValue.ToLower())));
            }
            catch (Exception)
            {

            }
        }
    }
}
