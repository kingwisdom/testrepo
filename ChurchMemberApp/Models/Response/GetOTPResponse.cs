using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class GetOTPResponse
    {
        public bool status { get; set; }
        public string response { get; set; }
        public OTPObject returnObject { get; set; }
    }

    public class OTPObject
    {
        public string otp { get; set; }
    }
}
