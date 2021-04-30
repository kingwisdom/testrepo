using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Popups;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage()
        {
            InitializeComponent();
            Init();
        }

        private MediaFile _mediaFile;
        private string URL { get; set; }

        private void Init()
        {

            email.Text = Preferences.Get("userName", string.Empty);
            name.Text =  Preferences.Get("fullName", string.Empty);
        }

        void back_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void update_Clicked(object sender, EventArgs e)
        {

            var model = new UpdateProfile()
            {
                name = name.Text,
                email = email.Text,
                phone = phone.Text,
                pictureUrl = UploadedUrl.Text,
                id = Preferences.Get("userId", string.Empty),
                tenantID = App.AppKey
            };
            var result = await ApiServices.UpdateUserInfo(model);
            if (result)
            {
                Preferences.Set("token", string.Empty);
                Preferences.Set("userId", string.Empty);
                Preferences.Set("userName", string.Empty);
                MessageDialog.Show("Success", "Your Profile Updated Successfully", "Ok");
                await Shell.Current.GoToAsync("../..");
            }
            else
            {
                MessageDialog.Show("Oops !!!", "Error occured while updating your profile", "Ok");
            }
        }

        private async void upload_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not support on your device.", "OK");
                return;
            }
            else
            {
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null) return;
                // imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                UploadImage(_mediaFile.GetStream());
            }
        }

        private async void UploadImage(Stream stream)
        {
            imgloading.IsVisible = true;
            update.IsEnabled = false;
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=churchplusstorage;AccountKey=rQZhE5UYZ9EdgzVZvr3bXLMNYEuoQG3jGW71uQFVeKxI+YR3iBlRyLWMxqOGTT83L3/6jRBH8uSSZkC3oJ+duA==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("churchpluscore");
            await container.CreateIfNotExistsAsync();
            var name = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{name}.png");
            await blockBlob.UploadFromStreamAsync(stream);
            URL = blockBlob.Uri.OriginalString;
            UploadedUrl.Text = URL;
            imgloading.IsVisible = false;
            update.IsEnabled = true;

            imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
            await DisplayAlert("Uploaded", "Image uploaded to Blob Storage Successfully!", "OK");
        }
    }
}