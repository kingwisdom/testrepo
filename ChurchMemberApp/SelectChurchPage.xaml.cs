using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views;
using ChurchMemberApp.Views.Pages;
using ChurchMemberApp.Views.Popups;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectChurchPage : ContentPage
    {
        ObservableCollection<Churches> ChurchList;
        ChurchProfile Profile { set; get; }
        public SelectChurchPage()
        {
            InitializeComponent();

            GetListOfChurches();

            Profile = new ChurchProfile();
        }

        private async void GetListOfChurches()
        {
            indicator.IsVisible = true;
            var res = await ApiServices.GetAllChurches();
            ChurchList = new ObservableCollection<Churches>();
            foreach (var item in res)
            {
                if(!string.IsNullOrWhiteSpace(item.churchName))
                    ChurchList.Add(item);
            }

            indicator.IsVisible = false;
            church.ItemsSource = ChurchList.OrderBy(e=>e.churchName);

           // if(response != null)       
        }

        private void select_Clicked(object sender, EventArgs e)
        {

        }

       private void Entry_TextChanged(object sender, TextChangedEventArgs e) { 
           
       }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            //var newChurchSearchList = ChurchList.Where(c => c.churchName.Trim().ToLower().Contains("techteam")).AsEnumerable(); //as ObservableCollection<Churches>;
            //church.ItemsSource = newChurchSearchList;

            //var searchResult = ChurchList.Where(er => er.churchName.ToLower().Contains(search.Text.ToLower()));
            indicator.IsVisible = true;
            var searchResult = ChurchList.Where(er => er.churchName.ToLower().Contains(e.NewTextValue.ToLower()));
            church.ItemsSource = searchResult;
            indicator.IsVisible = false;
        }

        private async void church_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var tenantId = (e.CurrentSelection.FirstOrDefault() as Churches)?.tenantId;
                    var name = (e.CurrentSelection.FirstOrDefault() as Churches)?.churchName;

                    Preferences.Set("tenantId", tenantId);
                    Preferences.Set("name", name);
                   // await ApiServices.GetAllChurchFeeds(tenantId, Preferences.Get("userId", string.Empty));
                    var res = await ApiServices.GetChurchProfile(tenantId);
                    if (res)
                    {
                        MessagingCenter.Send<SelectChurchPage>(this, "load");
                        var result = Preferences.Get("churchprofile", string.Empty);
                        if (result != string.Empty)
                        {
                            //Profile = new ChurchProfile(JsonConvert.DeserializeObject<ChurchProfile>(result));
                            Profile = JsonConvert.DeserializeObject<ChurchProfile>(result);
                            var colo = Profile.churchAppBackgroundColor != string.Empty ? Profile.churchAppBackgroundColor : "000";
                            Preferences.Set("appThemeColor", colo);

                            //if (string.IsNullOrEmpty(colo))
                            //{
                            //    Application.Current.Resources["PrimaryColor"] = Color.Green;
                            //}
                            //else
                            //{
                            Application.Current.Resources["PrimaryColor"] = Color.FromHex(Preferences.Get("appThemeColor", string.Empty));
                            // }
                        }
                        else
                        {
                            Application.Current.Resources["PrimaryColor"] = Color.FromHex("#000");
                            App.Current.MainPage = new SettingUpPage();
                        }
                    }

                    //if (string.IsNullOrEmpty(Preferences.Get("appThemeColor", string.Empty)))
                    //{
                    //    Application.Current.Resources["PrimaryColor"] = Color.Green;
                    //}
                    //else
                    //{
                    //    Application.Current.Resources["PrimaryColor"] = Color.FromHex("#" + Preferences.Get("appThemeColor", string.Empty));
                    //}
                    App.Current.MainPage = new SettingUpPage();
                }
                else
                {
                    MessageDialog.Show("Error!", "Please Check Your Internet, and try again", "Cancel");
                }
            }
            catch(Exception ex)
            {

                
            }

        }
    }
}