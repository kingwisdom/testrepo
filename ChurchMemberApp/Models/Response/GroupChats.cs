using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ChurchMemberApp.Models.Response
{
    public class GroupChats : ObservableObject
    {
        static Random Random = new Random();

        public string ChatMessageId { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
        public string TenantId { get; set; }

        private string _message;

        public string Text
        {
            get { return _message; }
            set => SetProperty(ref _message, value);
        }

        public object MediaUrl { get; set; }
        //public string senderName { get; set; }

        string user;
        public string SenderName
        {
            get => user;
            set => SetProperty(ref user, value);
        }

        string firstLetter;
        public string FirstLetter
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(firstLetter))
                    return firstLetter;

                firstLetter = SenderName?.Length > 0 ? SenderName[0].ToString() : "?";
                return firstLetter;
            }
            set => firstLetter = value;
        }


        Color color;
        public Color Color
        {
            get
            {
                if (color != null && color.A != 0)
                    return color;

                color = Color.FromRgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255)).MultiplyAlpha(.9);
                return color;
            }
            set => color = value;
        }

        Color backgroundColor;
        public Color BackgroundColor
        {
            get
            {
                if (backgroundColor != null && backgroundColor.A != 0)
                    return backgroundColor;

                backgroundColor = Color.MultiplyAlpha(.6);
                return backgroundColor;
            }
            set => backgroundColor = value;
        }

    }
}
