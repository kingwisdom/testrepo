using ChurchMemberApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace ChurchMemberApp.Helpers
{
    public static class Settings
    {
        public static string AppCenterAndroid = "AC_ANDROID";
        static readonly string defaultIP = ApiServices.basicUrl;

        public static bool UseHttps
        {
            get => (defaultIP != "localhost" && defaultIP != "10.0.2.2");
        }
        public static string ServerIP
        {
            get => Preferences.Get(nameof(ServerIP), defaultIP);
            set => Preferences.Set(nameof(ServerIP), value);
        }

        static Random random = new Random();
        static readonly string defaultName = $"{DeviceInfo.Platform} User {random.Next(1, 100)}";
        public static string UserName
        {
            get => Preferences.Get(nameof(UserName), defaultName);
            set => Preferences.Set(nameof(UserName), value);
        }


        public static string Group
        {
            get => Preferences.Get(nameof(Group), string.Empty);
            set => Preferences.Set(nameof(Group), value);
        }
    }
}
