using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace ChurchMemberApp.Views.Popups
{
    public partial class MessageDialog : PopupPage
    {
        public MessageDialog(string title, string message, string okText, Action okAction = null, string cancelText = null, Action cancelAction = null)
        {
            InitializeComponent();
            SetUp(title, message, okText, okAction, cancelText, cancelAction);
        }

        private void SetUp(string title, string message, string okText, Action okAction, string cancelText, Action cancelAction)
        {
            CloseWhenBackgroundIsClicked = false;
            TitleTxt.Text = title;
            MessageTxt.Text = message;

            okBtn.Text = okText;

            cancelBtn.IsVisible = !string.IsNullOrWhiteSpace(cancelText) ? true : false;
            cancelBtn.Text = cancelText;

            okBtn.Clicked += async (s, e) =>
            {
                await Navigation.PopPopupAsync().ConfigureAwait(false);
                okAction?.Invoke();
            };
            cancelBtn.Clicked += async (s, e) =>
            {
                await Navigation.PopPopupAsync().ConfigureAwait(false);
                cancelAction?.Invoke();
            };
        }

        public static void Show(string title, string message, string okText, Action okAction, string cancelText, Action cancelAction = null)
        {
            MessageDialog mes = new MessageDialog(title, message, okText, okAction, cancelText, cancelAction);
            App.Current.MainPage.Navigation.PushPopupAsync(mes);
        }

        public static void Show(string title, string message, string okText, Action okAction = null)
        {
            MessageDialog mes = new MessageDialog(title, message, okText, okAction);
            App.Current.MainPage.Navigation.PushPopupAsync(mes);
        }
    }
}
