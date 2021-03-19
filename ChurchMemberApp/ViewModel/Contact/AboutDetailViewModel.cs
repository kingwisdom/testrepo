using ChurchMemberApp.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace ChurchMemberApp.ViewModel.Contact
{
    public class AboutDetailViewModel : BaseViewModel
    {
        

        public AboutDetailViewModel()
        {
            Profile = new ChurchProfile();
            Abouts = new ObservableCollection<About>();
            Poulate();
        }

        private ChurchProfile _profile;

        public ChurchProfile Profile
        {
            get { return _profile; }
            set { _profile = value; }
        }


        private ObservableCollection<About> _about;

        public ObservableCollection<About> Abouts
        {
            get { return _about; }
            set { _about = value; }
        }

        void Poulate()
        {
            var res = Preferences.Get("churchprofile", string.Empty);
            Profile = JsonConvert.DeserializeObject<ChurchProfile>(res);
            foreach (var item in Profile.abouts.OrderBy(r => r.order))
            {
                Abouts.Add(item);
            }
        }

    }
}
