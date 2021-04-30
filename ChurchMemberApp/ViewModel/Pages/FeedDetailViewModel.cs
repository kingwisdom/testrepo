using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Pages
{
    public class FeedDetailViewModel : BaseViewModel
    {
        private Feeds _feed;

        public Feeds Feed
        {
            get { return _feed; }
            set { _feed = value; }
        }

        
        public FeedDetailViewModel(Feeds feeds)
        {
            this.Feed = feeds;
            Height = (feeds.comments.Count * 40) + (feeds.comments.Count * 20);
            imgHeight = feeds.mediaUrl == null ? 0 : 300;
        }

        private string _comment;
        public string comment
        {
            get { return _comment; }
            set { _comment = value;
                OnPropertyChanged(); }
        }
        private int h;
        public int imgHeight
        {
            get { return h; }
            set { h = value; OnPropertyChanged(); }
        }
        private int w;
        public int imgWidth
        {
            get { return w; }
            set { w = value; OnPropertyChanged(); }
        }

        int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand => new Command(() =>
        {
            if (IsRefreshing)
                return;
            IsRefreshing = true;
            //Feed.comments.Clear();
            GetFeeds();
            IsRefreshing = false;
        });

        async void GetFeeds()
        {
            ApiServices services = new ApiServices();
            var result = await services.GetAllChurchFeeds(App.AppKey, Preferences.Get("userId", string.Empty));
            var res = result.Where(e => e.postId == Feed.postId).FirstOrDefault();

            Feed.comments = res.comments;
        }

        public ICommand PostCommentCommand => new Command(async () =>
        {
            IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
                {
                    await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                else
                {

                    var request = new PostCommentReq()
                    {
                        commenterName = Preferences.Get("fullName", string.Empty),
                        commentMessage = comment,
                        postId = Feed.postId.ToString()
                    };
                    var res = await ApiServices.PostComment(request);
                    if (res)
                    {

                        await App.Current.MainPage.DisplayAlert("", "Comment made successfully", "ok");
                        comment = "";
                        RefreshCommand.Execute(null);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Error occur", "ok");
                    }
                }
            }
            catch (Exception)
            {
            }
        });


        //LikedCommand
        public ICommand LikedCommand => new Command(async (obj) =>
        {
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            else
            {
                var likeModel = new LikeRequest
                {
                    mobileUserID = Preferences.Get("userId", string.Empty),
                    postId = Feed.postId.ToString()
                };
                 //services = new ApiServices();
                var res = await ApiServices.LikePost(likeModel);
                if (res)
                {
                    if (Feed.isLiked)
                    {
                        Feed.isLiked = false;
                        if (Feed.likeCount <= 0)
                        {
                            Feed.likeCount = 0;
                            return;
                        }
                        else
                        {
                            //Feed.likeCount--;
                            Feed.colorChange = "Gray";

                            Feed.likeColor = Feed.colorChange;
                        }
                    }
                    else
                    {
                        //Feed.likeCount++;
                        Feed.isLiked = true;
                        Feed.colorChange = "Red";

                        Feed.likeColor = Feed.colorChange;
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
                await Share.RequestAsync(new ShareTextRequest
                {
                    Subject = Feed.content,
                    Text = Feed.content,
                    Title = Feed.title,
                    Uri = Feed.mediaUrl
                });

                var res = await ApiServices.PostShare(Feed.postId.ToString());
                if (res)
                    Feed.CommentCount++;
            }
        });
    }
}
