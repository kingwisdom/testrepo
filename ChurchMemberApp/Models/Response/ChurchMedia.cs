using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class ChurchMedia
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public MediaType mediaType { get; set; }
        public string category { get; set; }
        public DateTime dateAdded { get; set; }
        public string imagePath { get; set; }
        public string filePath { get; set; }
        public bool @public { get; set; }
        public int viewCount { get; set; }
        public int downloadCount { get; set; }
        public int sortOrder { get; set; }
        public double price { get; set; }
        public string fileBlobName { get; set; }
        public string imageBlobName { get; set; }
        public bool isFree { get; set; }
        public string tenantID { get; set; }
        public bool isPushed { get; set; }
    }

    public enum MediaType
    {
        Video,
        Audio,
        Ebook,
        Picture
    }
}
