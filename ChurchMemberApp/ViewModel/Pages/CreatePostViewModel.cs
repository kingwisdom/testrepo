using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Pages;
using ChurchMemberApp.Views.Popups;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Pages
{
   
    public class CreatePostViewModel : BaseViewModel
    { 
        
        Imessaging message { get; set; }
        ObservableCollection<GetPostCategory> Gets;
        public CreatePostViewModel()
        {
            IsNotBusy = true;
            AllCategory();
            Gets = new ObservableCollection<GetPostCategory>();
            Categories = Gets;
            message = DependencyService.Get<Imessaging>();
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

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
       

        string picture;
        public string Picture
        {
            get
            {
                return picture;
            }
            set { picture = value; OnPropertyChanged(); }
        }

        ImageSource _source;
        public ImageSource source
        {
            get { return _source; }
            set { _source = value; OnPropertyChanged(); }
        }

        private string _video;

        public string VideoPath
        {
            get { return _video; }
            set { _video = value; OnPropertyChanged(); }
        }


        private Stream _picturestream;
        public Stream picturestream
        {
            get { return _picturestream; }
            set { _picturestream = value; OnPropertyChanged(); }
        }

        private string _picturepath;
        public string PicturePath
        {
            get { return _picturepath; }
            set { _picturepath = value; OnPropertyChanged(); }
        }

        private ObservableCollection<GetPostCategory> category;
        public ObservableCollection<GetPostCategory> Categories
        {
            get { return category; }
            set { category = value; }
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
                    Gets.Add(item);
                }
            }
        }

        public ICommand GetCategoryCommand => new Command(async() => 
        {
            var res = await ApiServices.GetAllPostCategory();
            if(res != null)
            {

            }
        });

        private MediaFile _mediaFile;
        private string URL { get; set; }

        [Obsolete]
        public ICommand TakePicture => new Command(async () => 
        {
            await PopupNavigation.PopAsync();
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":(No Camera available.)", "OK");
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
                source = ImageSource.FromStream(() => _mediaFile.GetStream());
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                PicturePath = "Image URL:";
            }

            UploadImage(_mediaFile.GetStream());

        });

        [Obsolete]
        public ICommand PickPicture => new Command(async() => pickPicture());
        

        [Obsolete]
        private async void pickPicture()
        {
            try
            {
                await PopupNavigation.PopAsync();
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
                    source = ImageSource.FromStream(() => _mediaFile.GetStream());
                    
                    PicturePath = "Image URL:";
                }

                UploadImage(_mediaFile.GetStream());


            }
            catch (Exception ex)
            {

            }
        }

        private async void UploadImage(Stream stream)
        {
            IsBusy = true;
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=churchplusstorage;AccountKey=rQZhE5UYZ9EdgzVZvr3bXLMNYEuoQG3jGW71uQFVeKxI+YR3iBlRyLWMxqOGTT83L3/6jRBH8uSSZkC3oJ+duA==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("churchpluscore");
            await container.CreateIfNotExistsAsync();
            var name = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{name}.png");
            await blockBlob.UploadFromStreamAsync(stream);
            URL = blockBlob.Uri.OriginalString;
            PicturePath = URL;
            Preferences.Set("picUrl", URL);
            IsBusy = false;
            await App.Current.MainPage.DisplayAlert("Uploaded", "Image uploaded to Blob Storage Successfully!", "OK");
        }



        private async void UploadVideo(Stream stream)
        {

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
            VideoPath = URL;
            Preferences.Set("picUrl", URL);
            //NotBusy();
            await App.Current.MainPage.DisplayAlert("Uploaded", "Video uploaded Successfully!", "OK");
        }

        
        public ICommand PostCommand => new Command(async () =>
        {
            IsBusy = true;
            IsNotBusy = false;
            try
            {
                var post = new UserPostRequest
                {
                    title = "New Post from mobile",
                    content = Content,
                    mediaUrl = Preferences.Get("picUrl",string.Empty),
                    tenantId = App.AppKey,
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
                    await Shell.Current.GoToAsync("../..");
                }
                else
                {
                    MessageDialog.Show("Error!", "Error occured while saving Post", "Ok");
                }

                IsBusy = false;
                IsNotBusy = true;
            }
            catch (Exception)
            {
                IsBusy = false;


                IsNotBusy = true;
            }
        });

        public ICommand RecordVideoCommand => new Command(async () =>
        {
            await Permissions.RequestAsync<Permissions.StorageRead>();
            await Permissions.RequestAsync<Permissions.StorageWrite>();
            _mediaFile = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
            {
                SaveToAlbum = true,
                Directory = "Sample",
                DefaultCamera = CameraDevice.Front,
                DesiredLength = TimeSpan.FromSeconds(90),
                Quality = VideoQuality.Medium
            });



            if (_mediaFile == null)
                return;
            var stream = _mediaFile.GetStream();
            VideoPath = _mediaFile.AlbumPath;

            UploadVideo(stream);
            //await App.Current.MainPage.DisplayAlert("File Location", file.AlbumPath, "OK");
        });
    }

    
}
