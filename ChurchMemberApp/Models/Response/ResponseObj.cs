using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class ResponseObj
    {
        public bool status { get; set; }
        public string response { get; set; }
        public object returnObject { get; set; }
    }
}
