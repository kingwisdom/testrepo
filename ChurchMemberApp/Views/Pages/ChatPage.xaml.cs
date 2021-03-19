using ChurchMemberApp.Models.Response;
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
    public partial class ChatPage : ContentPage
    {
        GroupChatViewModel vm;
        public ChatPage()
        {
            InitializeComponent();
        }
        
        public ChatPage(ChurchGroups model)
        {
            InitializeComponent();
            BindingContext = vm = new GroupChatViewModel(model);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            if (!DesignMode.IsDesignModeEnabled)
                vm.ConnectCommand.Execute(null);
            vm.LoadItemsCommand.Execute(null);
        }

        

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (!DesignMode.IsDesignModeEnabled)
                vm.DisconnectCommand.Execute(null);

        }

    }
}