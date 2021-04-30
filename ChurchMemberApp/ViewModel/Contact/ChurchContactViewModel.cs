 using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Popups;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace ChurchMemberApp.ViewModel.Contact
{
    public class ChurchContactViewModel : BaseViewModel
    {
        [Obsolete]
        public ChurchContactViewModel()
        {
            Profile = new ChurchProfile();
            About = new ObservableCollection<About>();
            Pastors = new ObservableCollection<Pastor>();
            Banks = new ObservableCollection<Bank>();
            GetProfile();
        }

        private ChurchProfile _profile;
        public ChurchProfile Profile
        {
            get { return _profile; }
            set { _profile = value; OnPropertyChanged(); }
        }

        string _churchName;
        public string ChurchName
        {
            get { return _churchName; }
            set { _churchName = value; OnPropertyChanged(); }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(); }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }
        private string _logo;

        public string Logo
        {
            get { return _logo; }
            set { _logo = value; OnPropertyChanged(); }
        }


        private About _selectedAbout;

        public About SelectedAbout
        {
            get { return _selectedAbout; }
            set { _selectedAbout = value; OnPropertyChanged(); }
        }

        private Pastor _selectedPastor;

        public Pastor SelectedPastor
        {
            get { return _selectedPastor; }
            set { _selectedPastor = value; OnPropertyChanged(); }
        }


        private ObservableCollection<About> _about;

        public ObservableCollection<About> About
        {
            get { return _about; }
            set { _about = value; }
        }

        private ObservableCollection<Pastor> pastors;

        public ObservableCollection<Pastor> Pastors
        {
            get { return pastors; }
            set { pastors = value; }
        }

        private ObservableCollection<Bank> banks;

        public ObservableCollection<Bank> Banks
        {
            get { return banks; }
            set { banks = value; }
        }

        [Obsolete]
        public async void GetProfile()
        {
            await PopupNavigation.PushAsync(new Indicator());

            try
            {
                await ApiServices.GetChurchProfile(App.AppKey);

                try
                {
                    var res = Preferences.Get("churchprofile", string.Empty);
                    Profile = JsonConvert.DeserializeObject<ChurchProfile>(res);
                }
                catch (Exception)
                {
                }
                //if (Connectivity.NetworkAccess == NetworkAccess.Internet && string.IsNullOrEmpty(res))
                //{
                //    await ApiServices.GetChurchProfile(Preferences.Get("tenantId", string.Empty));
                //    return;
                //}
               
                ChurchName = Profile.churchName;
                Logo = Profile.logo;
                Email = Profile.email;
                Phone = Profile.phoneNumber;
                Address = Profile.address;

                foreach (var item in Profile.abouts.OrderBy(r=>r.order))
                {
                    About.Add(item);
                }

                foreach (var item in Profile.pastors)
                {
                    Pastors.Add(item);
                }

                foreach (var item in Profile.banks)
                {
                    Banks.Add(item);
                }

                await PopupNavigation.PopAsync();
            }
            catch (Exception ex)
            {
                await PopupNavigation.PopAsync();
                MessageDialog.Show("Error!", "Error occured ", "Cancel");

            }
        }

    }
}
