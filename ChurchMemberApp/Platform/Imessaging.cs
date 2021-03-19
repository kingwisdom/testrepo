using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.Platform
{
    public interface Imessaging
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
