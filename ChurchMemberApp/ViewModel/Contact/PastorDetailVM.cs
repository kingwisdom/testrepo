using ChurchMemberApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChurchMemberApp.ViewModel.Contact
{
    public class PastorDetailVM : BaseViewModel
    {
        public PastorDetailVM(Pastor pastor)
        {
            Pastor = pastor;
            Socials = new ObservableCollection<SocialMedia>();
            foreach (var item in Pastor.socialMedias)
            {
                Socials.Add(item);
            } 
        }

        private Pastor _pastor;

        public Pastor Pastor
        {
            get { return _pastor; }
            set { _pastor = value; }
        }

        private ObservableCollection<SocialMedia> _social;

        public ObservableCollection<SocialMedia> Socials
        {
            get { return _social; }
            set { _social = value; OnPropertyChanged(); }
        }


    }
}
