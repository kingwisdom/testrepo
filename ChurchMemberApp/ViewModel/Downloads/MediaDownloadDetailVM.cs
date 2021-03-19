using ChurchMemberApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChurchMemberApp.ViewModel.Downloads
{
    public class MediaDownloadDetailVM : BaseViewModel
    {
        public MediaDownloadDetailVM(DownloadedMedia media)
        {
            this.DownloadedMedia = media;
        }

        private DownloadedMedia _dmedia;

        public DownloadedMedia DownloadedMedia
        {
            get { return _dmedia; }
            set { _dmedia = value; }
        }

    }
}
