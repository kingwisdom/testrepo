using System;
using System.Linq;
using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Giving;
using ChurchMemberApp.Views.Media;
using ChurchMemberApp.Views.Pages;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.FirebasePushNotification;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Poppins-Bold.otf", Alias = "BoldFont")]
[assembly: ExportFont("Poppins-SemiBold.otf", Alias = "SemiBoldFont")]
[assembly: ExportFont("Poppins-Medium.otf", Alias = "MediumFont")]
[assembly: ExportFont("Poppins-Light.otf", Alias = "LightFont")]
[assembly: ExportFont("Poppins-Regular.otf", Alias = "RegularFont")]
[assembly: ExportFont("fa-solid-900.ttf", Alias = "FontIcon")]
[assembly: ExportFont("materialdesignicons.ttf", Alias = "MaterialIcons")]


namespace ChurchMemberApp
{
    public partial class App : Application
    {
        public static bool videostart = false;
        public static string videourl;
        public static WebView videoView;

        private static DB.DB _database;
        public App()
        {
            InitializeComponent();

            //Application.Current.Resources["PrimaryColor"] = Color.FromHex("#fb2056");

            CrossFirebasePushNotification.Current.Subscribe("media" + AppKey);
            CrossFirebasePushNotification.Current.Subscribe("feed" + AppKey);

            // Token event
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };
            //Push message received event
            //CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Opened");
            //    foreach (var data in p.Data)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //    }

            //};

            MainPage = new SplashPage();

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                var pagefrompush = p.Data["page"].ToString();
                var pushtitle = p.Data["pushtitle"].ToString();
                var pushdat = p.Data["pushpath"].ToString();
                var pushdet = p.Data["pushdetails"].ToString();
                var pushothers = "";
                var pushimagelink = p.Data["pushimagelink"].ToString();

                if (p.Data["pushothers"] != null)
                {
                    pushothers = p.Data["pushothers"].ToString();
                };

                MainPage = new MediaPage(pagefrompush, pushtitle, pushdat, pushdet, pushothers, pushimagelink);

            };


            MessagingCenter.Send<App>(this, "loadFeed");
                
        }

        public static DB.DB Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new DB.DB(DependencyService.Get<IFileHelper>().GetLocalFilePath("MChurch.db3"));
                }
                return _database;

            }
        }

        public static DownloadedMedia TobePlayed { get; set; }
        public static ChurchMedia TobeStreamed { get; set; }

        public DownloadedMedia RandomAudio
        {
            get
            {
                var random = App.Database.GetDownloadedAudio().Result.FirstOrDefault();
                return random;
            }
        }


        public const string USEREMAIL = "userName";
        public const string USERID = "userId";
        public const string USERSIGNEDINKEY = "token";


        //public static string AppKey { get { return "6128ede7-c982-4a3a-0e37-08d8eee83881"; } }
        //sarah church
         //public static string AppKey { get { return "e9749fad-85e8-4130-b553-37acc8acde61"; } }

        //porter's house
        public static string AppKey { get { return Preferences.Get("tenantId", string.Empty); } }
        protected async override void OnStart()
        {
            AppCenter.Start("android=e81e3897-e433-490b-b2ff-2417b8f8495a;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
            AppCenter.Start("android=e81e3897-e433-490b-b2ff-2417b8f8495a;" +
                              "uwp={Your UWP App secret here};" +
                              "ios={Your iOS App secret here}",
                              typeof(Analytics), typeof(Crashes));


            // MessagingCenter.Send<App>(this, "loadFeed");
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return;
            }
            if (string.IsNullOrEmpty(App.USERSIGNEDINKEY))
                return;

            await ApiServices.GetAllPostCategory();
            ApiServices services = new ApiServices();
            await services.GetAllChurchFeeds(AppKey);
            await ApiServices.GetChurchProfile(AppKey);


            CrossFirebasePushNotification.Current.Subscribe("media" + AppKey);
            CrossFirebasePushNotification.Current.Subscribe("feed" + AppKey);

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };
        }

        protected override void OnSleep()
        {
            CrossFirebasePushNotification.Current.Subscribe("media" + AppKey);
            CrossFirebasePushNotification.Current.Subscribe("feed" + AppKey);
            
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };
        }

        protected override void OnResume()
        {
            CrossFirebasePushNotification.Current.Subscribe("media" + AppKey);
            CrossFirebasePushNotification.Current.Subscribe("feed" + AppKey);

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };
        }

    }
}
