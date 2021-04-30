using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ChurchMemberApp.Models
{
    public class Feeds :INotifyPropertyChanged
    {
      
        public string postId { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string type { get; set; }

        public string mediaUrl 
        {
            get; set;
        }

        //Video
        private ImageSource vidImage;
        public ImageSource VidImage
        {
            get { return vidImage; }
            set { vidImage = value; OnPropertyChanged(); }
        }

        private string _vid;
        public string videoUrl
        {
            get {
                if (type == "Video") { _vid = mediaUrl; }
                else { _vid = ""; }
                return _vid; 
            }
            set { _vid = value; OnPropertyChanged(); OnPropertyChanged(nameof(VidImage)); }
        }

        private string _img;
        public string imageUrl
        {
            get { 
                if(type == "Picture")
                {
                    _img = mediaUrl;
                }
                //else if (type == "not set")
                //{
                //    try
                //    {
                //        VidImage = message.GenerateThumbImage(mediaUrl, 1);
                //    }
                //    catch (Exception r)
                //    {
                //    }
                //}
                //else { VidImage = message.GenerateThumbImage(mediaUrl,1); }
                //else { _img = "https://hopecottage.org/hc/wp-content/uploads/2017/06/video-icon-placeholder.png"; }
                return _img; 
            }
            set { _img = value; OnPropertyChanged(); }
        }

        public string content { get; set; }
        private long _shareCount;

        public long shareCount
        {
            get { return _shareCount; }
            set { _shareCount = value; OnPropertyChanged(); }
        }

        private int _height;
        public int ImageHight
        {
            get { 
                if(type == "Picture")
                {
                    return 200;
                }
                else
                {
                    return 0;
                }
                //return 0; 
            }
            set { _height = value; OnPropertyChanged(); }
        }
        private int _vheight;
        public int VHight
        {
            get { 
                if(type == "Video")
                {
                    _vheight= 200;
                }
                else
                {
                    IsVideo = false;
                    _vheight = 0;
                }
                return _vheight; 
            }
            set { _vheight = value; OnPropertyChanged(); }
        }

        private bool isVideo;

        public bool IsVideo
        {
            get 
            {
                return isVideo;
            }
            set { isVideo = value; OnPropertyChanged(); }
        }


        private long _likecount;
        public long likeCount
        {
            get { 
                return _likecount; }
            set { 
                
                _likecount = value; 
                OnPropertyChanged(); 
            }
        }
        private bool isliked;

        public bool isLiked
        {
            get {
                return isliked;
            }
            set { isliked = value; OnPropertyChanged(); }
        }

        public string colorChange { get; set; }

        private string _likeColor;
        public string likeColor
        {
            get {
                if (isLiked)
                {
                    _likeColor = "Red";
                }
                else
                {
                    _likeColor = "Gray";
                }
                return _likeColor;
            }
            set { _likeColor = value; OnPropertyChanged(); OnPropertyChanged(colorChange); }
        }

        private List<Comment> _comment;
        public List<Comment> comments {
            get { return _comment;}
            set {
                _comment = value; OnPropertyChanged();
            } 
        }
        public string postCategoryId { get; set; }
        public string postCategoryName { get; set; }
        public string PostCategoryImage { get; set; }
        public string postCategoryImage 
        {
            get
            {
                if (postCategoryName.ToLower() == "testimony")
                {
                    return "https://www.pngitem.com/pimgs/m/196-1961444_testimonial-icon-png-transparent-png.png";
                }
                else if (postCategoryName.ToLower() == "announcement")
                {
                    return "https://static.vecteezy.com/system/resources/thumbnails/001/760/457/small/megaphone-loudspeaker-making-announcement-vector.jpg";
                }
                else if (postCategoryName.ToLower() == "prayer point")
                {
                    return "https://img.freepik.com/free-vector/flat-praying-hands-background_23-2148005157.jpg?size=338&ext=jpg";
                }
                else
                {
                    return  "";
                }
                //return "";
            }
            set => PostCategoryImage = value;
        }
        public string tenantId { get; set; }
        public bool isApproved { get; set; }
        public PosterDetails posterDetails { get; set; }
        public long CommentCount
        {
            get
            {
                if (comments.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return comments.Count;
                }
            }
            set { OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    public class PosterDetails
    {
        public string posterName { get; set; }
        public string posterImageUrl { get; set; }
    }

}
