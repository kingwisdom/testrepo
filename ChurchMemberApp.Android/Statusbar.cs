using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChurchMemberApp.Droid;
using ChurchMemberApp.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(Statusbar))]
namespace ChurchMemberApp.Droid
{
    public class Statusbar : IStatusBarPlatformSpecific
    {
        public Statusbar()
        {
        }

        public void SetStatusBarColor(Color color)
        {
            // your color manipulating code here!
            // scroll to get my statusbar color chaning code
        }
    }
}