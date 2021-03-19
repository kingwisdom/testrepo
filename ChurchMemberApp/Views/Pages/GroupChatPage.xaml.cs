using ChurchMemberApp.Helpers;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using ChurchMemberApp.ViewModel.Pages;
using ChurchMemberApp.Views.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupChatPage : ContentPage
    {
        ObservableCollection<ChurchGroups> ChurchGroup;
        //GroupChatViewModel vm;
        public GroupChatPage()
        {
            InitializeComponent();
            GetChurchGroups();
            //BindingContext=vm = new GroupChatViewModel();
        }

        private async void GetChurchGroups()
        {
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                await Navigation.PushModalAsync(new LoginPage());

                return;
            }
            else
            {

                indicator.IsVisible = true;
                var res = await ApiServices.GetAllChurchGroup();
                ChurchGroup = new ObservableCollection<ChurchGroups>();
                foreach (var item in res)
                {
                    if (!string.IsNullOrWhiteSpace(item.groupName))
                        ChurchGroup.Add(item);
                }

                indicator.IsVisible = false;
                group.ItemsSource = ChurchGroup.OrderBy(e => e.groupName);
            }
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    //
        //    vm.LoadItemsCommand.Execute(null);
        //}

        private void group_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var group = (e.CurrentSelection.FirstOrDefault() as ChurchGroups);
            Settings.Group = group.groupName;
            Navigation.PushAsync(new ChatPage(group));
        }

        
    }
}