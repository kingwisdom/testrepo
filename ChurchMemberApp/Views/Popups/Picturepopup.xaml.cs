using ChurchMemberApp.ViewModel.Pages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Picturepopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        CreatePostViewModel vm;
        public Picturepopup(string source)
        {
            InitializeComponent();
            image.Source = source;
            BindingContext = new CreatePostViewModel();
            MessagingCenter.Subscribe<CreatePostViewModel, string>(this, "Pic", (sender, args) =>
              {
                  image.Source = args;
              });
        }
    }
}