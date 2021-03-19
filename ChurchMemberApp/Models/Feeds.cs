using ChurchMemberApp.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ChurchMemberApp.Models
{
    public class Feeds :INotifyPropertyChanged
    {
        public string postId { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string mediaUrl { get; set; }

        public string content { get; set; }
        private long _shareCount;

        public long shareCount
        {
            get { return _shareCount; }
            set { _shareCount = value; OnPropertyChanged(); }
        }


        private long _likecount;
        public long likeCount
        {
            get { return _likecount; }
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
                    postCategoryName = "";
                }
                return "";
            }
            set => PostCategoryImage = value;
        }
        public string tenantId { get; set; }
        public bool isApproved { get; set; }

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
}
