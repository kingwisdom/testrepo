using ChurchMemberApp.Models;
using ChurchMemberApp.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedDetailPage : ContentPage
    {
        FeedDetailViewModel model;
        public FeedDetailPage()
        {
            InitializeComponent();
        }

        public FeedDetailPage(Feeds feed)
        {
            InitializeComponent();
            model = new FeedDetailViewModel(feed);
            this.BindingContext = model;
        }
        public FeedDetailPage(string pushtitle, string pushdate, string pushdetails)
        {
            InitializeComponent();
            var Feed = new Feeds()
            {
                title = pushtitle,
                content = pushdetails
            };
            model = new FeedDetailViewModel(Feed);
            this.BindingContext = model;
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}