using ChurchMemberApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChurchMemberApp.ViewModel.Media
{
    public class YoutubeViewModel : BaseViewModel
    {
        private const string ApiKey = "AIzaSyCdeGtSM5WIruQ0kis2cXr339PZwZwZdlI";
        private string channelApiUrl = "https://www.googleapis.com/youtube/v3/search?part=id&maxResults=30&channelId=" + "UCZ-cLPSHShEZYPVA58NJtBQ" + "&key=" + ApiKey;
        private string playlistApiUrl = "";


        // private string apiUrlForChannelNextPage = "https://www.googleapis.com/youtube/v3/search?part=id&order=date&pageToken=" + NextPageToken + "&maxResults=40&channelId=" + channelID + "&key=" + ApiKey;
        private string apiUrlForVideosDetails = "https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics&id=" + "{0}" + "&key=" + ApiKey;


        //https://youtube.googleapis.com/youtube/v3/liveStreams?key="ApiKey
        private List<Youtube> youtubeItems;
        public List<Youtube> YoutubeItems
        {
            get { return youtubeItems; }
            set {  youtubeItems = value; OnPropertyChanged(); }
        }

        private Youtube youtube;

        public Youtube SelectItem
        {
            get { return youtube; }
            set { youtube = value; }
        }

        private Youtube _youtube;

        public Youtube LatestVideo
        {
            get { return _youtube; }
            set { _youtube = value; OnPropertyChanged(); }
        }


        ObservableCollection<Youtube> _Videos;
        public ObservableCollection<Youtube> Videos
        {
            get { return _Videos; }
            set {  _Videos= value; OnPropertyChanged(); }
        }

        public YoutubeViewModel()
        {
            InitDataAsync();
            YoutubeItems = new List<Youtube>();
            Videos = new ObservableCollection<Youtube>();
        }
        public async Task InitDataAsync()
        {
            var videoIds = await GetVideoFromChannel();
        }

        private async Task<List<string>> GetVideoFromChannel()
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(channelApiUrl);
            var videoIds = new List<string>();
            try
            {
                JObject response = JsonConvert.DeserializeObject<dynamic>(json);
                var items = response.Value<JArray>("items");
                foreach (var item in items)
                {
                    videoIds.Add(item.Value<JObject>("id")?.Value<string>("videoId"));
                }
                YoutubeItems = await GetVideosDetailsAsync(videoIds);


            }
            catch (Exception)
            {

                throw;
            }
            return videoIds;
        }

        private async Task<List<Youtube>> GetVideosDetailsAsync(List<string> videoIds)
        {
            var videoIdsString = "";
            foreach (var s in videoIds)
            {
                videoIdsString += s + ",";
            }
            var client = new HttpClient();
            var json = await client.GetStringAsync(string.Format(apiUrlForVideosDetails, videoIdsString));
            var youtubeItems = new List<Youtube>();
            try
            {
                JObject response = JsonConvert.DeserializeObject<dynamic>(json);
                var items = response.Value<JArray>("items");
                foreach (var item in items)
                {
                    var snippet = item.Value<JObject>("snippet");
                    var statistics = item.Value<JObject>("statistics");
                    var youtubeItem = new Youtube
                    {
                        Title = snippet.Value<string>("title"),
                        Description = snippet.Value<string>("description"),
                        ChannelTitle = snippet.Value<string>("channelTitle"),
                        PublishedAt = snippet.Value<DateTime>("publishedAt"),
                        VideoId = item?.Value<string>("id"),
                        DefaultThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("default")?.Value<string>("url"),
                        MediumThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("medium")?.Value<string>("url"),
                        HighThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("high")?.Value<string>("url"),
                        StandardThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("standard")?.Value<string>("url"),
                        MaxResThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("maxres")?.Value<string>("url"),

                        ViewCount = statistics?.Value<int>("viewCount"),
                        LikeCount = statistics?.Value<int>("likeCount"),
                        DislikeCount = statistics?.Value<int>("dislikeCount"),
                        FavoriteCount = statistics?.Value<int>("favoriteCount"),
                        CommentCount = statistics?.Value<int>("commentCount"),

                        //Tags = (from tag in snippet?.Value<JArray>("tags") select tag.ToString())?.ToList()
                    };
                    YoutubeItems.Add(youtubeItem);

                }
                foreach (var videos in YoutubeItems)
                {
                    Videos.Add(videos);
                }
                LatestVideo = Videos.First();
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("", ex.Message, "Cancel");
            }
            return YoutubeItems;
        }


    }
}
