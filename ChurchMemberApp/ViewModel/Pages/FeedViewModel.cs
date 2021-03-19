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
        Imessaging message { get; set; }

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
            GetFeeds();
            IsRefreshing = false;
        });

        public FeedViewModel()
        {
            message = DependencyService.Get<Imessaging>();
            Feeds = new ObservableCollection<Feeds>();

            MessagingCenter.Subscribe<SelectChurchPage>(this, "load", (sender) =>
            {
                GetFeedsCommand.Execute(null);
            });

            GetFeeds();  
        }


        public void GetFeeds()
        {
            try
            {
                IsBusy = true;

                
                if (App.AppKey != string.Empty)
                {
                    ///ApiServices services = new ApiServices();
                    //var result = await services.GetAllChurchFeeds(App.AppKey, Preferences.Get("userId", string.Empty));
                    var json = Preferences.Get("AllFeeds", string.Empty);
                    var result = JsonConvert.DeserializeObject<IEnumerable<Feeds>>(json);
                    if (result != null)
                    {
                        
                        foreach (var item in result)
                        {
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
                    Uri = share.mediaUrl
                });

                var res = await ApiServices.PostShare(share.postId.ToString());
                if (res)
                    share.CommentCount++;
            }
        });


    }

    
    
}
