using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class PasswordOTPResponse
    {
        public bool status { get; set; }
        public string response { get; set; }
        public ReturnObject returnObject { get; set; }
    }

    public class ReturnObject
    {
        public string resettoken { get; set; }
    }

}
