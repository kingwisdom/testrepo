using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChurchMemberApp.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WebView), typeof(PdfWebViewRenderer))]
	namespace ChurchMemberApp.Droid
	{
		public class PdfWebViewRenderer : WebViewRenderer
		{
			public PdfWebViewRenderer(Context context) : base(context)
			{
			}

			protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
			{
				base.OnElementChanged(e);

				if (e.NewElement != null)
				{
					Control.Settings.AllowFileAccess = true;
					Control.Settings.AllowFileAccessFromFileURLs = true;
					Control.Settings.AllowUniversalAccessFromFileURLs = true;
				}
			}
		}
	}
