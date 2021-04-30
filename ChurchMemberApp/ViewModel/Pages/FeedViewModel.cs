using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Platform;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Pages;
using ChurchMemberApp.Views.Popups;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Pages
{
    public class FeedViewModel : BaseViewModel
    {
        public static FeedViewModel instance;
        Imessaging message { get; set; }
        IVImages vconvert { get; set; }

        bool searchListIsvisible;
        public bool SearchlistIsvisible
        {
            get
            {
                return searchListIsvisible;
            }
            set
            {
                searchListIsvisible = value; OnPropertyChanged();
            }
        }

        public ObservableCollection<Feeds> SearhList { get; set; }


        ObservableCollection<Feeds> feeds;
        public ObservableCollection<Feeds> Feeds
        {
            get { return feeds; }
            set { feeds = value; OnPropertyChanged(); OnPropertyChanged(nameof(Feeds));  }
        }

        public List<Feeds> NewFeeds { get; set; }
        //public int CommentCount { get; set; }
        private Feeds _selectFeed;

        public Feeds SelectFeed
        {
            get { return _selectFeed; }
            set { _selectFeed = value; OnPropertyChanged(); }
        }


        public ICommand RefreshCommand => new Command(() =>
        {
            if (IsRefreshing)
                return;
            IsRefreshing = true;
            // GetFeeds();
            GetFeedsCommand.Execute(null);

             IsRefreshing = false;
        });

        public void OnAppearing()
        {
            IsBusy = true;
            SelectFeed = null;
        }

        async void OnItemSelected(Feeds item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(FeedDetailPage)}?{nameof(FeedDetailViewModel.Feed.postId)}={item.postId}");
            await App.Current.MainPage.Navigation.PushAsync(new FeedDetailPage(item));
        }
        public Command<Feeds> ItemTapped { get; }
        public FeedViewModel()
        {
            message = DependencyService.Get<Imessaging>();
            Feeds = new ObservableCollection<Feeds>();
            ItemTapped = new Command<Feeds>(OnItemSelected);
            vconvert = DependencyService.Get<IVImages>();
            MessagingCenter.Subscribe<SelectChurchPage>(this, "load", (sender) =>
            {
                GetFeedsCommand.Execute(null);
            });
            MessagingCenter.Subscribe<App>(this, "loadFeed", (sender) =>
            {
                GetFeedsCommand.Execute(null);
            });

            instance = this;

            GetFeeds();
            
        }


        public void GetFeeds()
        {
            try
            {
                IsBusy = true;

                
                if (App.AppKey != string.Empty)
                {
                    var json = Preferences.Get("AllFeeds", string.Empty);
                    var result = JsonConvert.DeserializeObject<IEnumerable<Feeds>>(json);
                    if (result != null)
                    {
                        
                        foreach (var item in result)
                        {
                            if(item.type == "Video")
                            {
                                try
                                {
                                    //item.VidImage = vconvert.GenerateThumbImage(item.mediaUrl, 1);
                                    item.VidImage = "https://investorpresentations.co.za/videos/placeholder_1080.jpg";
                                }
                                catch(Exception ex)
                                {
                                }
                            }
                            Feeds.Add(item);
                        }
                    }
                }
                IsBusy = false;

               //await PopupNavigation.PopAsync();
            }
            catch (Exception ex)
            {
                //await PopupNavigation.PopAsync();
                IsBusy = false;
                MessageDialog.Show("Error!", "Error occured while getting feeds", "Cancel");
            }
        }

        public ICommand GetFeedsCommand => new Command(async () =>
        {
            IsBusy = true;
            try
            {
                ApiServices services = new ApiServices();
                var result = await services.GetAllChurchFeeds(App.AppKey, Preferences.Get("userId",string.Empty));
                if (result != null)
                {
                    
                    Feeds.Clear();
                    foreach (var item in result)
                    {
                        Feeds.Add(item);
                    }
                   // message.LongAlert("Feed loaded successfully");
                }
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        });

        //LikedCommand
        public ICommand LikedCommand => new Command(async(obj) =>
        {
            
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            else
            {
            
               var feed = obj as Feeds;

                var likeModel = new LikeRequest
                {
                    mobileUserID = Preferences.Get("userId", string.Empty),
                    postId = feed.postId
                };

                var res =  await ApiServices.LikePost(likeModel);
               

                if (res)
                {
                    if (feed.isLiked)
                    {
                        feed.isLiked = false;
                        if (feed.likeCount <= 0)
                        {
                            feed.likeCount = 0;
                            return;
                        }
                        else
                        {
                            feed.likeCount--;
                            feed.colorChange = "Gray";

                            feed.likeColor = feed.colorChange;
                        }
                    }
                    else
                    {
                        feed.likeCount++;
                        feed.isLiked = true;
                        feed.colorChange = "Red";

                        feed.likeColor = feed.colorChange;
                    }

                    
                }
                //await ApiServices.LikePost(likeModel);

            }
        });

        public ICommand ShareCommand => new Command(async (obj) =>
        {
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            else
            {
                var share = obj as Feeds;
                await Share.RequestAsync(new ShareTextRequest
                {
                    Subject = share.content,
                    Text = share.content,
                    Title = share.title,
                    Uri = "https://churchplus.co"
                });

                var res = await ApiServices.PostShare(share.postId.ToString());
                if (res)
                    share.CommentCount++;
            }
        });


    }

    
    
}
