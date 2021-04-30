using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Request
{
    public class UpdateProfile
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string tenantID { get; set; }
        public string pictureUrl { get; set; }
        public string pictureUlrBlobName { get; set; }
        public string imageBase64string { get; set; }
        public string imageExtension { get; set; }
    }
}
