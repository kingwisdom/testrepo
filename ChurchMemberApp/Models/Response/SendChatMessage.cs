using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Models.Response
{
    public class SendChatMessage
    {
        public string chatMessageId { get; set; }
        public string senderId { get; set; }
        public string recieverId { get; set; }
        public string tenantId { get; set; }
        public string text { get; set; }
        public string mediaUrl { get; set; }
        public string senderName { get; set; }
    }
}
