using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Giving;
using ChurchMemberApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Giving
{
    public class ContributionItemVM : BaseViewModel
    {
        public ContributionItemVM(PaymentForm model)
        {
            this.Payment = model;
            //OrderId = model.orderId;
            ContributionItems = new ObservableCollection<ContributionItem>(Payment.contributionItems);
            Gateways = new ObservableCollection<GateWay>(Payment.gateWays);
            
        }

        private PaymentForm _pay;
        //public string OrderId { get; set; }
        public PaymentForm Payment
        {
            get { return _pay; }
            set { _pay = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ContributionItem> contributions;

        public ObservableCollection<ContributionItem> ContributionItems
        {
            get { return contributions; }
            set { contributions = value; }
        }
        
        private ObservableCollection<GateWay> gatways;

        public ObservableCollection<GateWay> Gateways
        {
            get { return gatways; }
            set { gatways = value; }
        }
        
        

        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }

        public ICommand PayOptionCommand => new Command(async () =>
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddPaymentMethodPage(Payment));
        });

        [Obsolete]
        public ICommand PaystackCommand => new Command(async () =>
        {
            await PopupNavigation.PushAsync(new Indicator());
            decimal total = 0;
            List<Contribution> fa = new List<Contribution>();
            foreach (var item in ContributionItems)
            {
                var cont = new Contribution()
                {
                    contributionItemName = item.contributionName,
                    amount = item.Amount,
                    contributionCurrencyId = Payment.currencyId,
                    contributionItemId = item.financialContributionID
                };


               fa.Add(cont);
               total = total + item.Amount;
            };
            var response = fa.Sum(er => er.amount);
            DonationRequest donation = new DonationRequest()
            {
                name = Payment.name,
                email = Preferences.Get("userName", string.Empty),
                orderID = Payment.orderId,
                tenantID = App.AppKey,
                phone = "",
                paymentFormId = Payment.id,
                isAnonymous = true,
                contributionItems = fa.ToList(),
                currencyId = Payment.currencyId
            };

            var req = await ApiServices.PostPaymentRequest(donation);
            //call paystack and services at the same time
            if (req)
            {
               await App.Current.MainPage.Navigation.PushModalAsync(new Paystack(response, Payment));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "There is a problem, please try again", "Yes");
            }
        });


        public ICommand PayResponseCommand => new Command(async () =>
        {
            //decimal total = 0;
            List<Contribution> fa = new List<Contribution>();
            foreach (var item in ContributionItems)
            {
                var cont = new Contribution()
                {
                    contributionItemName = item.contributionName,
                    amount = item.Amount,
                    contributionCurrencyId = Payment.currencyId,
                    contributionItemId = item.financialContributionID
                };


                fa.Add(cont);
                //total = total + item.Amount;
            };

            ConfirmDonation donation = new ConfirmDonation()
            {
                name = Payment.name,
                email = Preferences.Get("userName", string.Empty),
                orderID = Payment.orderId,
                tenantID = App.AppKey,
                phone = "",
                paymentFormId = Payment.id,
                isAnonymous = string.IsNullOrWhiteSpace(Preferences.Get("token", string.Empty)) ? true : false,
                contributionItems = fa.ToList(),
                currencyId = Payment.currencyId
            };

            await ApiServices.PostConfirmDonation(donation);
            if (true)
            {
                //MessageDialog.Show("", "Thank you for your payment", "ok");
                //await App.Current.MainPage.Navigation.PushModalAsync(new Paystack(response, Payment));
            }
            else
            {
                //await App.Current.MainPage.DisplayAlert("", "There is a problem, please try again", "Yes");
            }
        });

        //token

    }
}
