using AVFoundation;
using ChurchMemberApp.Platform;
using CoreGraphics;
using CoreMedia;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ChurchMemberApp.iOS.VideoImages))]
namespace ChurchMemberApp.iOS
{
    class VideoImages : IVImages
    {
        public ImageSource GenerateThumbImage(string url, long usecond)
        {
            AVAssetImageGenerator imageGenerator = new AVAssetImageGenerator(AVAsset.FromUrl((new Foundation.NSUrl(url))));
            imageGenerator.AppliesPreferredTrackTransform = true;
            CMTime actualTime;
            NSError error;
            CGImage cgImage = imageGenerator.CopyCGImageAtTime(new CMTime(usecond, 1000000), out actualTime, out error);
            return ImageSource.FromStream(() => new UIImage(cgImage).AsPNG().AsStream()); 
            //ImageSource.FromStream(() => new UIImage(cgImage).AsPNG().AsStream());
        }

    }
}