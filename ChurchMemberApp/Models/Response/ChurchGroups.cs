using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChurchMemberApp.Models.Response
{
    public class ChurchGroups
    {
        
        public string chatGroupId { get; set; }
        public string tenantId { get; set; }
        public object messages { get; set; }
        public object members { get; set; }
        public string groupName { get; set; }
        public DateTime dateCreated { get; set; }
        private string date;
        public string Date
        {
            get
            {
                return dateCreated.ToString("dd/MM/yyyy");
            }
        }
        public string groupLogoUrl { get; set; }


    }
}
