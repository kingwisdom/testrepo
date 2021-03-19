using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Request
{
    public class PostCommentReq
    {
        public string postId { get; set; }
        public string commentMessage { get; set; }
        public string commenterName { get; set; }
    }
}
