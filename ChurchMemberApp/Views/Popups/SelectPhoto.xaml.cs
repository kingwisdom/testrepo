using ChurchMemberApp.ViewModel.Pages;
using Rg.Plugins.Popup.Pages;
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
    public partial class SelectPhoto : PopupPage
    {
        public SelectPhoto()
        {
            InitializeComponent();
            BindingContext = new CreatePostViewModel();
        }

    }
}