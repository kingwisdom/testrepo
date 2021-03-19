using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Core.EventHandlers
{
    public class MessageEventArgs : IMessageEventArgs
    {
        public MessageEventArgs(string message, string user)
        {
            Message = message;
            User = user;
        }

        public string Message { get; }
        public string User { get; }
    }
}
