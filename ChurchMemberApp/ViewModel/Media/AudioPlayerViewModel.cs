using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using ChurchMemberApp.Views.Media;
using MediaManager;
using MediaManager.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Media
{
    public class AudioPlayerViewModel : BaseViewModel
    {
        private static AudioPlayerViewModel _instance;
        public static AudioPlayerViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
        private ChurchMedia _Item;
        public ChurchMedia Item
        {
            get { return _Item; }
            set { _Item = value; OnPropertyChanged(); }
        }
        IFileHelper dependency = DependencyService.Get<IFileHelper>();
        public bool IsNotPlaying { get { return !IsPlaying; } }

        public string DurationLabel => CrossMediaManager.Current.Duration.ToString(@"hh\:mm\:ss");

        public bool IsPlaying => CrossMediaManager.Current.IsPlaying();

        public AudioPlayerViewModel(ChurchMedia audio, string str)
        {
            Item = new ChurchMedia();
            Item = audio;
            //Item = new DownloadedAudio(audio);
            AddToQueue();
            checkmediaqueu();
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.StateChanged += Current_StateChanged;
            Duration = 1000;
            _instance = this;
            MessagingCenter.Subscribe<MediaDetailPage, ChurchMedia>(this, "Stream", (sender, args) =>
            {
                
                IsBusy = true;
                App.TobeStreamed = args;
                var medi = new MediaItem(args.filePath)
                {
                    Title = args.name,
                    Artist = args.name,

                };
                StreamAudiofile(medi);
            });

        }


        private void Current_StateChanged(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsPlaying));
            OnPropertyChanged(nameof(IsNotPlaying));
            OnPropertyChanged(nameof(DurationLabel));
        }
        public ICommand PlayCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CrossMediaManager.Current.MediaPlayer.Play();
                });
            }
        }
        public ICommand PlayPrev
        {
            get
            {
                return new Command(async () =>
                {
                    await CrossMediaManager.Current.PlayPrevious();
                });
            }
        }
        public ICommand PlayNext
        {
            get
            {
                return new Command(async () =>
                {
                    await CrossMediaManager.Current.PlayNext();
                });
            }
        }
        public ICommand PauseCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CrossMediaManager.Current.Pause();
                });
            }
        }
       // IFileHelper dependency = DependencyService.Get<IFileHelper>();
        //Imessaging message = DependencyService.Get<Imessaging>();
        private double _Progress;
        public double Progress
        {
            get { return _Progress; }
            set
            {
                _Progress = value; OnPropertyChanged();
            }
        }
        private double _Position;
        public double Position
        {
            get { return _Position; }
            set
            {
                _Position = value; OnPropertyChanged();
            }
        }
        private string _PositionLabel;
        public string PositionLabel
        {
            get { return _PositionLabel; }
            set
            {
                 _PositionLabel =value; OnPropertyChanged();
            }
        }

        public ICommand SeekCompleted
        {
            get
            {
                return new Command(() =>
                {
                    CrossMediaManager.Current.SeekTo(TimeSpan.FromSeconds(Position));
                });
            }
        }
        private double _Duration;
        public double Duration
        {
            get { return _Duration; }
            set
            {
                 _Duration = value; OnPropertyChanged();
            }
        }
        private void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            try
            {
                PositionLabel = e.Position.ToString(@"hh\:mm\:ss");
                Position = e.Position.TotalSeconds;
                if (Position > 0)
                {
                    Duration = CrossMediaManager.Current.Duration.TotalSeconds;
                    IsBusy = false;

                    MessagingCenter.Send(this, "shownotification");
                }
            }
            catch (Exception ex)
            {


            }

        }
        public async void GetAudioFile(string audiofileid)
        {
            try
            {
                var dir = dependency.GetLocalFilePath();
                var fn = $"{dir}/{audiofileid}.mp3";
                FileInfo file = new FileInfo(fn);
                await CrossMediaManager.Current.Play(file);
            }
            catch (Exception)
            {

            }

        }
        public async void StreamAudiofile(MediaItem mediaitem)
        {
            try
            {
                await CrossMediaManager.Current.Play(mediaitem);
            }
            catch (Exception)
            {


            }

        }

        public void checkmediaqueu()
        {
            var jj = CrossMediaManager.Current.Queue.Count;
            Debug.WriteLine("the number if item in queue is" + jj.ToString());
        }
        public async void AddToQueue()
        {
            try
            {
                var extractor = CrossMediaManager.Current.Extractor;
                CrossMediaManager.Current.Queue.Clear();
                var downloads = App.Database.GetDownloadedAudio().Result;

                foreach (var item in downloads)
                {
                    var dir = dependency.GetLocalFilePath();
                    var fn = $"{dir}/{item.name}.mp3";
                    FileInfo file = new FileInfo(fn);
                    CrossMediaManager.Current.Queue.Add(await extractor.CreateMediaItem(file));
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        private string Timify(double time)
        {
            String ret = "--:--";
            int hour, min, second;
            if (time < 0)
                return ret;

            if (time < 3600)
            {
                min = (int)(time / 60);
                second = (int)(time % 60);
                ret = $"{min.ToString("00")}:{second.ToString("00")}";
            }
            else
            {
                hour = (int)(time / 3600);
                time /= 60;
                min = (int)(time / 60);
                second = (int)(time % 60);
                ret = $"{hour.ToString("00")}:{min.ToString("00")}:{second.ToString("00")}";
            }
            return ret;
        }
    }
}
