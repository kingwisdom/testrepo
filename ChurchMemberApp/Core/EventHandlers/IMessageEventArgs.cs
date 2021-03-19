using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Core.EventHandlers
{
    public interface IMessageEventArgs
    {
        string Message { get; }
    }
}
