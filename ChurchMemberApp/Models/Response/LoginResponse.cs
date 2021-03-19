using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class LoginResponse
    {
        public string token { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string userId { get; set; }
        public DateTime expiryTime { get; set; }
        public string tenantID { get; set; }
    }
}
