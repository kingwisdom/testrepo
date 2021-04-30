using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class CheckDataResponseObj
    {
        public bool status { get; set; }
        public string response { get; set; }
        public RetObject returnObject { get; set; }
    }
    public class RecordObj
    {
        public string personId { get; set; }
        public string phoneNumber { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string nameResult { get; set; }
        public string dayOfBirth { get; set; }
        public string monthOfBirth { get; set; }
        public object attendanceCode { get; set; }
    }

    public class RetObject
    {
        public object record1 { get; set; }
        public RecordObj record2 { get; set; }
    }

}
