using System;
using System.Linq;
using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using ChurchMemberApp.Views;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Giving;
using ChurchMemberApp.Views.Media;
using ChurchMemberApp.Views.Pages;
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
        private static DB.DB _database;
        public App()
        {
            InitializeComponent();

            Application.Current.Resources["PrimaryColor"] = Color.FromHex(Preferences.Get("appThemeColor", string.Empty));

            //Device.SetFlags(new[] { "Expander_Experimental"});
            //MainPage = new NavigationPage(new test());


            if (string.IsNullOrEmpty(AppKey))
            {
                MainPage = new NavigationPage(new OnboardPage());
            }
            else
            {
                MainPage = new MainShellPage();
            }

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


        public static string AppKey { get { return Preferences.Get("tenantId", string.Empty); } }
        protected override void OnStart()
        {
           // MessagingCenter.Send<App>(this, "loadFeed");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        
    }
}
