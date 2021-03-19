using ChurchMemberApp.iOS.Renderers;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditor))]
namespace ChurchMemberApp.iOS.Renderers
{
    class CustomEditor : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BackgroundColor = null;
                //Control. = UIKit.UITextBorderStyle.None;
            }
        }
    }
}