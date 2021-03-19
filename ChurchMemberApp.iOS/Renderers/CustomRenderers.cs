using System;
using ChurchMemberApp.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomRenderers))]
namespace ChurchMemberApp.iOS.Renderers
{
    public class CustomRenderers : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = null;
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }
    }
}
