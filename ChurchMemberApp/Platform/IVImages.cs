using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChurchMemberApp.Platform
{
    public interface IVImages
    {
        ImageSource GenerateThumbImage(string url, long usecond);
    }
}
