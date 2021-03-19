using ChurchMemberApp.Core;
using ChurchMemberApp.Helpers;
using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Pages
{
    public class GroupChatViewModel : BaseViewModel
    {
        ChatService chatService;
        HubConnection hubConnection;
        public ObservableCollection<GroupChats> Messages { get; set; }
        public GroupChats ChatMessage { get; }
        public Command LoadItemsCommand { get; set; }
        public ChurchGroups Groups { get; set; }
        public ObservableCollection<User> Users { get; }
        
        bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    isConnected = value; OnPropertyChanged();
                });
            }
        }

        public Command SendMessageCommand { get; }
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }

        Random random;

        private GroupChats _post;

        public GroupChats post
        {
            get { return _post; }
            set { _post = value; OnPropertyChanged(); }
        }
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }


        public GroupChatViewModel(ChurchGroups groups)
        {
            if (DesignMode.IsDesignModeEnabled)
                return;

            chatService = new ChatService();
            // Title = Settings.Group;
            ChatMessage = new GroupChats();
            Messages = new ObservableCollection<GroupChats>();
            Users = new ObservableCollection<User>();
            SendMessageCommand = new Command(async () => await SendMessage());
            ConnectCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());
            random = new Random();

            chatService.Init(ApiServices.basicUrl);

            chatService.OnReceivedMessage += (sender, args) =>
            {
                SendLocalMessage(args.Message, args.User);
                AddRemoveUser(args.User, true);
            };

            chatService.OnEnteredOrExited += (sender, args) =>
            {
                AddRemoveUser(args.User, args.Message.Contains("entered"));
            };

            chatService.OnConnectionClosed += (sender, args) =>
            {
                SendLocalMessage(args.Message, args.User);
            };

            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://ecofaith.azurewebsites.net/chat")
                .Build();


            Groups = groups;

            LoadItemsCommand = new Command(async()=> 
            {
                IsBusy = true;
                var res = await ApiServices.GetGroupChat(groups.chatGroupId);
                foreach (var item in res)
                {
                    Messages.Add(item);
                }
                IsBusy = false;
            });
            //ReceiveMessage

        }


        async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {
                IsBusy = true;
                await chatService.ConnectAsync();
                await chatService.JoinChannelAsync(Settings.Group, Settings.UserName);
                IsConnected = true;

                AddRemoveUser(Settings.UserName, true);
                await Task.Delay(500);
                SendLocalMessage("Connected...", Settings.UserName);
            }
            catch (Exception ex)
            {
                SendLocalMessage($"Connection error: {ex.Message}", Settings.UserName);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task Disconnect()
        {
            if (!IsConnected)
                return;
            await chatService.LeaveChannelAsync(Settings.Group, Settings.UserName);
            await chatService.DisconnectAsync();
            IsConnected = false;
            SendLocalMessage("Disconnected...", Settings.UserName);
        }

        async Task SendMessage()
        {
            if (!IsConnected)
            {
                await App.Current.MainPage.DisplayAlert("Not connected", "Please connect to the server and try again.", "OK");
                return;
            }
            try
            {
                IsBusy = true;
                await chatService.SendMessageAsync(Settings.Group,
                    Settings.UserName, ChatMessage.Text);

                ChatMessage.Text = string.Empty;

                var posts = new GroupChats()
                {
                    SenderName = Preferences.Get("fullName", string.Empty),
                    SenderId = Preferences.Get("userId", string.Empty),
                    TenantId = App.AppKey,
                    Text = Text,
                    RecieverId = Groups.chatGroupId
                };
                var res = await ApiServices.AddItemAsync(posts);
            }
            catch (Exception ex)
            {
                SendLocalMessage($"Send failed: {ex.Message}", Settings.UserName);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SendLocalMessage(string message, string user)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var first = Users.FirstOrDefault(u => u.Name == user);

                Messages.Insert(0, new GroupChats
                {
                    Text = message,
                    SenderName = user,
                   // Color = first?.Color ?? Color.FromRgba(0, 0, 0, 0)
                });
            });
        }

        void AddRemoveUser(string name, bool add)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;
            if (add)
            {
                if (!Users.Any(u => u.Name == name))
                {
                    var color = Messages.FirstOrDefault(m => m.SenderName == name)?.Color ?? Color.FromRgba(0, 0, 0, 0);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Users.Add(new User { Name = name, Color = color });
                    });
                }
            }
            else
            {
                var user = Users.FirstOrDefault(u => u.Name == name);
                if (user != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Users.Remove(user);
                    });
                }
            }
        }

        public ICommand PostCommand => new Command(async() =>
        {
            var posts = new GroupChats()
            {
                SenderName = Preferences.Get("fullName", string.Empty),
                SenderId = Preferences.Get("userId", string.Empty),
                TenantId = App.AppKey,
                Text = Text,
                RecieverId = Groups.chatGroupId
            };
            var res = await ApiServices.AddItemAsync(posts);
        });

        //private async Task SendMessage()
        //{
        //    var message = new GroupChats
        //    {
        //        senderId = "8852b1a5-d69e-4abf-b80a-3508e4217244",
        //        recieverId = "4be66ca6-4c99-45a7-7ff3-08d8e3c55d38",
        //        tenantId = "12efa63b-ae11-4e24-9b19-fb325d66442e",
        //        text = "Hope it is working now!",
        //        senderName = "Abah Joseph Israel"
        //    };

        //    Text = string.Empty;
        //   // Chats.Add(message);
        //    await hubConnection.SendAsync("SendMessage", message.text);
        //}
    }
}
