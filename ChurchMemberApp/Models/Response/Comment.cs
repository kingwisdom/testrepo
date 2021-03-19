using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class Comment
    {
        public string commentId { get; set; }
        public string postId { get; set; }
        public object post { get; set; }
        public DateTime commentDate { get; set; }
        public string commentMessage { get; set; }
        public string commenterName { get; set; }
        public bool isApproved { get; set; }
    }
}
