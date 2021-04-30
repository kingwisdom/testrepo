using ChurchMemberApp.Models.Response;
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
        public Picturepopup(ChurchMedia media)
        {
            InitializeComponent();
            image.Source = media.filePath;
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("../..");
        }
    }
}