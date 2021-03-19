using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Pages;
using ChurchMemberApp.Views.Popups;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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
        ObservableCollection<GetPostCategory> Gets;
        public CreatePostViewModel()
        {
            IsNotBusy = true;
            AllCategory();
            Gets = new ObservableCollection<GetPostCategory>();
            Categories = Gets;
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
        private string mediaUrl;

        public string MediaUrl
        {
            get { return mediaUrl; }
            set { mediaUrl = value; }
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
            set {_picturepath = value; OnPropertyChanged();}
        }

        private ObservableCollection<GetPostCategory> category;
        public ObservableCollection<GetPostCategory> Categories
        {
            get { return category; }
            set { category = value; }
        }


       private async void AllCategory()
        {
            var res = await ApiServices.GetAllPostCategory();
            foreach (var item in res)
            {
                Gets.Add(item);
            }
        }

        //if (categoryIndex > -1)
        //        transactions.Category = Category[categoryIndex].Title;
        //    var result = await service.AddTransactionAsync(transactions);
        [Obsolete]
        public ICommand TakePicture => new Command(async () => 
        {
            await PopupNavigation.PopAsync();
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Samplepics",
                Name = "test.jpg"
            });

            if (_mediaFile == null)
                return;

            // await App.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
            source = ImageSource.FromStream(() => _mediaFile.GetStream());
            //source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    return stream;
            //});

           // UploadImage(_mediaFile.GetStream());
            //Picture = _mediaFile.Path;
        });


        public ICommand PickPicture => new Command(async() => takePicture());
        private MediaFile _mediaFile;
        public string URL { get; set; }

        [Obsolete]
        private async void takePicture()
        {
            try
            {
                await PopupNavigation.PopAsync();
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("", "You cant't take photo with this phone!.", "OK");
                    return;
                }
                var status = await Permissions.RequestAsync<Permissions.StorageRead>();
                await Permissions.RequestAsync<Permissions.StorageWrite>();
               // var status1 = await Permissions.RequestAsync<Permissions.Camera>();
                //var mediaOptions = new StoreCameraMediaOptions();

                if (status == PermissionStatus.Granted)
                {
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };
                    _mediaFile = await CrossMedia.Current.PickPhotoAsync(mediaOption);
                    if (_mediaFile == null) return;

                    source = ImageSource.FromStream(() => _mediaFile.GetStream());
                    
                    //picturestream = _mediaFile.GetStream();
                     
                    //UploadImage(_mediaFile.GetStream());
                    // VideoPath = null;
                    Picture = _mediaFile.Path;

                    //source = ImageSource.FromStream(() =>
                    //{
                    //    picturestream = selectedImageFile.GetStream();
                    //    selectedImageFile.Dispose();
                    //    return picturestream;
                    //});
                    if (source == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Could not take the picture, please try again.", "Ok");
                        return;
                    }
                    else
                    {
                        //await Application.Current.MainPage.DisplayAlert("Picture Selected", PicturePath, "OK");
                        //MessagingCenter.Send(this, "Pic", picture);
                        //IsPicture = true;
                    }
                    //isbusy = false;
                }


                //source = ImageSource.FromStream(() => picturestream);

            }
            catch (Exception ex)
            {

            }
        }

        //Upload to blob function    
        static string _storageConnection = "DefaultEndpointsProtocol=https;AccountName=churchplusstorage;AccountKey=rQZhE5UYZ9EdgzVZvr3bXLMNYEuoQG3jGW71uQFVeKxI+YR3iBlRyLWMxqOGTT83L3/6jRBH8uSSZkC3oJ+duA==;EndpointSuffix=core.windows.net";
        static CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConnection);
        static CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        static CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("churchpluscore");

        private async void UploadImage(Stream stream)
        {
            //var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=churchplusstorage;AccountKey=rQZhE5UYZ9EdgzVZvr3bXLMNYEuoQG3jGW71uQFVeKxI+YR3iBlRyLWMxqOGTT83L3/6jRBH8uSSZkC3oJ+duA==;EndpointSuffix=core.windows.net");
            //var client = account.CreateCloudBlobClient();
            //var container = client.GetContainerReference("churchpluscore");
            //await container.CreateIfNotExistsAsync();
            //var name = Guid.NewGuid().ToString();
            //var blockBlob = container.GetBlockBlobReference(name+Picture);
            //await blockBlob.UploadFromStreamAsync(stream);
            //URL = blockBlob.Uri.OriginalString;
            //Picture = URL;

            try
            {
                string filePath = _mediaFile.Path;
                string fileName = Path.GetFileName(filePath);
                await cloudBlobContainer.CreateIfNotExistsAsync();

                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
                var blockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                await Upload(blockBlob, filePath);
            }
            catch (StorageException e)
            {
                await App.Current.MainPage.DisplayAlert("",e.Message,"Ok");
            }

            //await DisplayAlert("Uploaded", "Image uploaded to Blob Storage Successfully!", "OK");
        }

        private async Task Upload(CloudBlockBlob blob, string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                await blob.UploadFromStreamAsync(fileStream);
            }
        }

        public ICommand ViewPicture => new Command(async () =>
        {
            if (picture != null)
            {
                await App.Current.MainPage.Navigation.PushPopupAsync(new Picturepopup(picture));
            }
            else
            {
                //take picture
                takePicture();
            }
        });
        public ICommand PostCommand => new Command(async () =>
        {
            IsBusy = true;
            IsNotBusy = false;
            try
            {

                if(_mediaFile == null)
                {
                    return;
                }

                var post = new UserPostRequest
                {
                    title = "Post from mobile",
                    content = Content,
                    mediaUrl = MediaUrl,
                    
                    tenantId = App.AppKey,
                    //image = Picture
                };

                if (postIndex > -1)
                {
                    post.postCategoryId = Categories[postIndex].postCategoryId;
                }


                var res = await ApiServices.UserPost(post);
                if (res)
                {
                    if(_mediaFile.GetStream() != null)
                        UploadImage(_mediaFile.GetStream());
                    MessageDialog.Show("Add Post", "Your Post has been saved successfully", "Ok");
                    App.Current.MainPage = new FeedPage();
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

            //Get the public album path

            if (_mediaFile == null)
                return;
            var stream = _mediaFile.GetStream();
            VideoPath = _mediaFile.AlbumPath;

            UploadImage(stream);
            //await App.Current.MainPage.DisplayAlert("File Location", file.AlbumPath, "OK");
        });
    }

    
}
