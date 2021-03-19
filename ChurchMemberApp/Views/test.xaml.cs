
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class test : ContentPage
    {
        public test()
        {
            InitializeComponent();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"
                    <html>
                    <body>
                    <h1 style='color:red;text-align:center'>My web view</h1>
                    </body>
                    </html>
                    ";
            yourWebView.Source = htmlSource;
        }
    }
}