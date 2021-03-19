using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class GetPostCategory
    {
        public string postCategoryId { get; set; }
        public string name { get; set; }
        public string postCategoryImage { get; set; }
        public object posts { get; set; }
        public string tenantId { get; set; }
    }
}
