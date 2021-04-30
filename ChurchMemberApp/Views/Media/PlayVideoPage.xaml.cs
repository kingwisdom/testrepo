using ChurchMemberApp.Models.Response;
using MediaManager;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChurchMemberApp.Views.Media
{
    public partial class PlayVideoPage : ContentPage
    {
        public PlayVideoPage()
        {
            InitializeComponent();
            
        }
        ChurchMedia model;
        public PlayVideoPage(ChurchMedia media)
        {
            InitializeComponent();
            model = media;
            Init(media);
           // CrossMediaManager.Current.Play(media.imagePath);
           
        }

        private void Init(ChurchMedia media)
        {
            videoPath.Text = media.filePath;
            title.Text = media.name;
            author.Text = media.category;
            videoImg.Source = media.imagePath;
            description.Text = media.description;
            date.Text = media.dateAdded.ToString("dddd, dd MMMM yyyy");
        }

        private void play_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new VLCPage(model));
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        
    }
}
