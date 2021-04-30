using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Lottie.Forms.Platforms.Android;
using ImageCircle.Forms.Plugin.Droid;
using MediaManager;
using LibVLCSharp.Forms.Shared;
using Plugin.FirebasePushNotification;

namespace ChurchMemberApp.Droid
{
    [Activity(Label = "Potter's House of Lagos", Icon = "@mipmap/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //AnimationViewRenderer.Init(this, savedInstanceState);
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            LibVLCSharpFormsRenderer.Init();
            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
           
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            SQLitePCL.Batteries_V2.Init();
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            ImageCircleRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            CrossMediaManager.Current.Init(this);
            LoadApplication(new App());
        }

        //protected override void OnNewIntent(Intent intent)
        //{
        //    FirebasePushNotificationManager.ProcessIntent(this, Intent);
        //}
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}