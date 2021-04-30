using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Media
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PdfReaderPage : ContentPage
    {


        readonly IFileHelper dependency = DependencyService.Get<IFileHelper>();
        readonly Imessaging message = DependencyService.Get<Imessaging>();
        public PdfReaderPage()
        {
            InitializeComponent();
        }
        
        public PdfReaderPage(string url)
        {
            InitializeComponent();
            PdfView.Source = url;
        }

        public PdfReaderPage(DownloadedMedia media)
        {
            InitializeComponent();

            try
            {
                var dir = dependency.GetLocalFilePath();
                var fn = $"{dir}/{media.id}.pdf";

                if (Device.RuntimePlatform == Device.Android)
                    PdfView.Source = $"file:///android_asset/pdfjs/web/viewer.html?file={WebUtility.UrlEncode(fn)}";
            }
            catch (Exception ex)
            {
            }
        }
    }
}