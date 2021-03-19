using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChurchMemberApp.Droid;
using ChurchMemberApp.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace ChurchMemberApp.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public string GetLocalFilePath()
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "MyMedia");
            return libFolder;
        }
        public string GetFilePath(string fileName)
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string _rootDir = Path.Combine(docFolder, "MyMedia");
            var filePath = Path.Combine(_rootDir, fileName);
            return File.Exists(filePath) == false ? "" : filePath;

        }
        public bool FileExist(string fileName)

        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string _rootDir = Path.Combine(docFolder, "MyMedia");
            var filePath = Path.Combine(_rootDir, fileName);

            return File.Exists(filePath);

        }

        public string pickfile(string filename)
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string _rootDir = Path.Combine(docFolder, "MyMedia");
            var filePath = Path.Combine(_rootDir, filename);
            if (File.Exists(filePath))
            {
                return filePath;
            }
            else
            {
                return "";
            }
        }

        public FileStream GetFileStream(string fileName)
        {
            FileStream memoryStream = null;
            try
            {
                string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string _rootDir = Path.Combine(docFolder, "MyMedia");
                var filePath = Path.Combine(_rootDir, fileName);
                memoryStream = new FileStream(filePath, FileMode.Open);
            }

            catch (Exception ex)
            {

            }

            return memoryStream;
        }
    }
}