using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using ChurchMemberApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Giving
{
    public class OfferingFormViewModel : BaseViewModel
    {
        Imessaging toast;
        public OfferingFormViewModel()
        {
            Oft = new ObservableCollection<PaymentForm>();
            GetForms();
            Transactions = new ObservableCollection<ContributionsReq>();
            UserContributions = new ObservableCollection<UserContributionList>();

            GetTransaction();
        }

        private async void GetForms()
        {
            //var res = Preferences.Get("paymentForm", string.Empty);
            var res = await ApiServices.GetOfferingForms();
            if (res != null)
            {
               // var result = JsonConvert.DeserializeObject<List<PaymentForm>>(res);
                foreach (var item in res)
                {
                    Oft.Add(item);
                }
            }
        }

        private PaymentForm _selectItem;
        public PaymentForm SelectItem
        {
            get { return _selectItem; }
            set { _selectItem = value; }
        }

        private ObservableCollection<PaymentForm> _oft;
        public ObservableCollection<PaymentForm> Oft
        {
            get { return _oft; }
            set { _oft = value; OnPropertyChanged(); OnPropertyChanged(nameof(Oft)); }
        }
        
        private ObservableCollection<UserContributionList> userContributions;
        public ObservableCollection<UserContributionList> UserContributions
        {
            get { return userContributions; }
            set { userContributions = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ContributionItem> citem;
        public ObservableCollection<ContributionItem> ContributionItems
        {
            get { return citem; }
            set { citem = value; }
        }

        private ObservableCollection<ContributionsReq> cr;

        public ObservableCollection<ContributionsReq> Transactions
        {
            get { return cr; }
            set { cr = value; }
        }



        public ICommand GetPaymentForms => new Command(async () =>
        {
            IsBusy = true;
            try
            {
                var res = await ApiServices.GetOfferingForms();
                if(res == null)
                {
                    Oft = null;
                    return;
                }
                

                //foreach (var item in res)
                //{
                //    Oft.Add(item);
                //}
                
            }
            catch (Exception ex)
            {
                toast.LongAlert(ex.Message);
                IsBusy = false;
            }
        });

        public ICommand TransactionCommand => new Command(async () =>
        {
            var model = new ContributionsReq()
            {
                tenantId = App.AppKey,
                userId = Preferences.Get("userId", string.Empty)
            };
            var result = await ApiServices.GetContributionsTrasaction(model);
            //if (result != null)
            //{
            //    foreach (var item in result)
            //    {
            //        UserContributions.Add(item);
            //    }
            //}
        });

        private void GetTransaction()
        {
            var res = Preferences.Get("ContributionsItems", string.Empty);
            var result = JsonConvert.DeserializeObject<List<UserContributionList>>(res);
            if (result != null)
            {
                foreach (var item in result)
                {
                    UserContributions.Add(item);
                }
            }
        }

    }
}
