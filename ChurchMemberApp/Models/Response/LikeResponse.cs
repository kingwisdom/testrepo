using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class LikeResponse
    {
        public string postId { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string mediaUrl { get; set; }
        public string content { get; set; }
        public int shareCount { get; set; }
        public int likeCount { get; set; }
        public object comments { get; set; }
        public List<Like> likes { get; set; }
        public string postCategoryId { get; set; }
        public object postCategory { get; set; }
        public string tenantId { get; set; }
        public bool isApproved { get; set; }
    }

    public class Like 
    { 
        public string likeID { get; set; } 
        public string mobileUserID { get; set; } 
        public string postID { get; set; } 
        public bool isLike { get; set; } 
    }
    
}
