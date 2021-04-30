using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChurchMemberApp.Platform
{
    public interface Imessaging
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
