using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChurchMemberApp.Platform
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
        string GetLocalFilePath();
        bool FileExist(string fileName);
        string GetFilePath(string fileName);
        string pickfile(string filename);

        FileStream GetFileStream(string fileName);
    }
}
