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
    public partial class MainShellPage : Shell
    {
        public MainShellPage()
        {
            InitializeComponent();
            MessagingCenter.Send<MainShellPage>(this, "loadFeed");
        }
        
    }
}