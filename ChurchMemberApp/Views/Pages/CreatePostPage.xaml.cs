using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using ChurchMemberApp.Services;
using ChurchMemberApp.ViewModel.Pages;
using ChurchMemberApp.Views.Popups;
using LibVLCSharp.Shared;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePostPage : ContentPage
    {
        Imessaging message { get; set; }
       // ObservableCollection<GetPostCategory> Categories;
        //CreatePostViewModel vm;
        public CreatePostPage()
        {
            InitializeComponent();
            BindingContext = this;
            message = DependencyService.Get<Imessaging>();
            //vm.GetCategoryCommand.Execute(null);
            Categories = new ObservableCollection<GetPostCategory>();
            username.Text = Preferences.Get("userName", string.Empty);
            AllCategory();
            
        }

        private ObservableCollection<GetPostCategory> category;
        public ObservableCollection<GetPostCategory> Categories
        {
            get { return category; }
            set { category = value; }
        }

        private int postIndex = -1;
        public int PostIndex
        {
            get { return postIndex; }
            set
            {
                postIndex = value;
            }
        }

        private MediaFile _mediaFile;
        private string URL { get; set; }
        private async void back_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("../..");
        }

        private void AllCategory()
        {
            //await ApiServices.GetAllPostCategory();
            var response = Preferences.Get("PostCategory", string.Empty);
            var church = JsonConvert.DeserializeObject<List<GetPostCategory>>(response);
            if (church != null)
            {
                foreach (var item in church)
                {
                    Categories.Add(item);
                }
                cat.ItemsSource = Categories;
            }
        }


        [Obsolete]
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new SelectPhoto());
        }

        private async void camera_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":(No Camera available.)", "OK");
                return;
            }
            else
            {
                
                _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "myImage.jpg"
                });

                if (_mediaFile == null) return;
                imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                UploadedUrl.Text = "Image URL:";

                if (_mediaFile != null)
                {
                    UploadImage(_mediaFile.GetStream());
                }
            }
        }

        private async void gallery_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Error", "This is not support on your device.", "OK");
                return;
            }
            else
            {
               
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Full
                };
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();

                if (_mediaFile == null) return;
                imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());

                UploadedUrl.Text = "Image URL:";

               
            }
            if (_mediaFile != null)
            {
                UploadImage(_mediaFile.GetStream());
            }
        }

        private async void video_Tapped(object sender, EventArgs e)
        {
            await Permissions.RequestAsync<Permissions.StorageRead>();
            await Permissions.RequestAsync<Permissions.StorageWrite>();
            _mediaFile = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
            {
                SaveToAlbum = true,
                Directory = "Sample",
                CompressionQuality = 50,
                DefaultCamera = CameraDevice.Front,
                DesiredLength = TimeSpan.FromSeconds(110),
                Quality = VideoQuality.Low
            });



            if (_mediaFile == null)
                return;
            var stream = _mediaFile.GetStream();
            UploadedUrl.Text = _mediaFile.AlbumPath;

            UploadVideo(stream);
        }

        private async void post_Clicked(object sender, EventArgs e)
        {
            loading.IsVisible = true;
            Busy();
            try
            {
                var post = new UserPostRequest
                {
                    title = postTitle.Text,
                    content = Content.Text,
                    mediaUrl = UploadedUrl.Text,
                    tenantId = App.AppKey,
                    posterId = Preferences.Get("userId", string.Empty),

                //image = Picture
                };

                //UploadImage(_mediaFile.GetStream());
                if (postIndex > -1)
                {
                    post.postCategoryId = Categories[postIndex].postCategoryId;
                }

                var res = await ApiServices.UserPost(post);
                if (res)
                {
                    //MessageDialog.Show("Add Post", "Your Post has been saved successfully", "Ok");
                    message.LongAlert("Your Post has been saved successfully");
                    MessageDialog.Show("Note", "Your Post is pending for approval, it will be visible after it has been approved", "Ok");
                    await Shell.Current.GoToAsync("../..");
                }
                else
                {
                    MessageDialog.Show("Error!", "Error occured while saving Post", "Ok");
                    await Shell.Current.GoToAsync("../..");
                }

                loading.IsVisible = false;
                NotBusy();
            }
            catch (Exception)
            {
                loading.IsVisible = false;

                NotBusy();
            }
        }


        private async void UploadImage(Stream stream)
        {
            uploadload.IsVisible = true;
            Busy();
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=churchplusstorage;AccountKey=rQZhE5UYZ9EdgzVZvr3bXLMNYEuoQG3jGW71uQFVeKxI+YR3iBlRyLWMxqOGTT83L3/6jRBH8uSSZkC3oJ+duA==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("churchpluscore");
            await container.CreateIfNotExistsAsync();
            var name = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{name}.png");
            await blockBlob.UploadFromStreamAsync(stream);
            URL = blockBlob.Uri.OriginalString;
            UploadedUrl.Text = URL;
            NotBusy();
            uploadload.IsVisible = false;
             message.LongAlert("Image uploaded to Storage Successfully!");
        }

        private async void UploadVideo(Stream stream)
        {
            Busy();
            uploadload.IsVisible = true;
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=churchplusstorage;AccountKey=rQZhE5UYZ9EdgzVZvr3bXLMNYEuoQG3jGW71uQFVeKxI+YR3iBlRyLWMxqOGTT83L3/6jRBH8uSSZkC3oJ+duA==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("churchpluscore");
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            await container.CreateIfNotExistsAsync();
            var name = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{name}.mp4");
            await blockBlob.UploadFromStreamAsync(stream);
            URL = blockBlob.Uri.OriginalString;
            UploadedUrl.Text = URL;
            Preferences.Set("picUrl", URL);

            uploadload.IsVisible = false;
            NotBusy();
            mymedia.IsVisible = true;
            DisplayVideo(URL);
            message.LongAlert("Video uploaded Successfully!");
        }

        LibVLC lib;
        private void DisplayVideo(string uRL)
        {
            LibVLCSharp.Shared.Core.Initialize();
            lib = new LibVLC();

            var media = new LibVLCSharp.Shared.Media(lib, uRL, FromType.FromLocation);
            mymedia.MediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true };
            mymedia.MediaPlayer.Play();
        }

        public void Busy()
        {
            
            post.IsEnabled = false;

        }

        public void NotBusy()
        {
            post.IsEnabled = true;

        }

        //private async void btnUpload_Clicked(object sender, EventArgs e)
        //{
        //    if (_mediaFile == null)
        //    {
        //        await DisplayAlert("Error", "There was an error when trying to get your image.", "OK");
        //        return;
        //    }
        //    else
        //    {
        //        UploadImage(_mediaFile.GetStream());
        //    }
        //}
    }
}