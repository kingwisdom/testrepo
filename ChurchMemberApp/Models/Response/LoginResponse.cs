using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class LoginResponse
    {
        public bool status { get; set; }
        public string response { get; set; }
        public LoginReturnObject returnObject { get; set; }
    }

    public class LoginReturnObject
    {
        public string token { get; set; }
        public string email { get; set; }
        public string fullname { get; set; }
        public string phoneNumber { get; set; }
        public string userId { get; set; }
        public DateTime expiryTime { get; set; }
        public string tenantID { get; set; }
    }

}
