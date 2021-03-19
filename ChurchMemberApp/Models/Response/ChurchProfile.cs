using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class ChurchProfile
    {
        public string churchName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string logo { get; set; }
        public string services { get; set; }
        public List<About> abouts { get; set; }
        public List<Pastor> pastors { get; set; }
        public List<Bank> banks { get; set; }
        public string churchAppBackgroundColor { get; set; }
    }

    public class About
    {
        public string title { get; set; }
        public string details { get; set; }
        public int order { get; set; }
    }

    public class SocialMedia
    {
        public string socialMediaId { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public string owner { get; set; }
    }

    public class Pastor
    {
        public string pastorId { get; set; }
        public string name { get; set; }
        public string bio { get; set; }
        public string level { get; set; }
        public string branch { get; set; }
        public List<SocialMedia> socialMedias { get; set; }
        public string tenantID { get; set; }
    }

    public class Bank
    {
        public string bankId { get; set; }
        public string bankName { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public string bankLogoUrl { get; set; }
        public string tenantID { get; set; }
    }
}
