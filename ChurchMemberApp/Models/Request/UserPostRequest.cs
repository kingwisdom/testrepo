using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Request
{
    public class UserPostRequest
    {
        public string title { get; set; }
        public string mediaFile { get; set; }
        public string mediaUrl { get; set; }
        public string content { get; set; }
        public string posterId { get; set; }
        public string postCategoryId { get; set; }
        public string tenantId { get; set; }
        public List<string> mediaChannels { get; set; } = null;
    }
}
