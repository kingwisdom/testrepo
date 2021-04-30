
using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using ChurchMemberApp.ViewModel.Giving;
using ChurchMemberApp.Views.Popups;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Giving
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Paystack : ContentPage
    {
        public Paystack()
        {
            InitializeComponent();

        }

        ContributionItemVM vm;
        public Paystack(decimal model, PaymentForm id)
        {
            InitializeComponent();
            // Init(model, id);
            vm = new ContributionItemVM(id);
             PopupNavigation.PopAsync();
            //Info = info;
            // var amount = info.TransactionCharge;


            var app = Application.Current as App;

            JArray jarray = new JArray();
            dynamic customFieldsArray = new JArray();

            dynamic displayname = new JObject();
            displayname.display_name = "Mobile Number";
            displayname.variable_name = "mobile_number";
            displayname.@value = "+2348012345678";
            customFieldsArray.Add(displayname);

            dynamic jobtitlefield = new JObject();
            jobtitlefield.display_name = "Job Title";
            jobtitlefield.variable_name = "job_title";
            customFieldsArray.Add(jobtitlefield);
            dynamic custom = new JObject();
            custom.custom_fields = customFieldsArray;

            dynamic product = new JObject();
            product.key = "pk_live_5066a61cbd1694ec8276b1205f9ebcd885c34701";
           // product.key = "pk_test_9a8895ede03716b9a0474fea6da11ec5bc1c7033";
            product.email = Preferences.Get("userName", string.Empty) == "" ? "churchplus@gmail.com" : Preferences.Get("userName", string.Empty);
            product.currency = "NGN";
            product.amount = model*100;
            // product.email = product.email.trim();
            //  product.subaccount = "";
            product.bearer = "account";
            product.@ref = id.orderId;//Info.PaymentReference;
            product.metadata = custom;
            try
            {
                hybridWebView.Data = product.ToString();


                hybridWebView.RegisterCloseAction(() => CloseMethod());

                hybridWebView.RegisterCallBackAction(data => ResponsePayment());
            }
            catch (Exception ex)
            {

            }

        }

        private async void Confirm()
        {
            MessageDialog.Show("Alert", "Payment successfull, God bless you", "OK");
            await Shell.Current.GoToAsync("../..");
        }

        private async void CloseMethod()
        {
            MessageDialog.Show("Stop", "You stoped payment process, please make payment again", "ok");

            await Shell.Current.GoToAsync("../..");
        }

        private async void Init(decimal mode, PaymentForm id)
        {
            await PopupNavigation.PopAsync();
            //loading.IsVisible = false;

            JArray jarray = new JArray();
            dynamic customFieldsArray = new JArray();

            dynamic displayname = new JObject();
            displayname.display_name = id.name;
            displayname.variable_name = id.name;
            displayname.@value = "+2348167927876";

            customFieldsArray.Add(displayname);
            dynamic jobtitlefield = new JObject();
            jobtitlefield.display_name = "Job Title";
            jobtitlefield.variable_name = "job_title";
            jobtitlefield.@value = "Software Developer";

            customFieldsArray.Add(jobtitlefield);



            dynamic custom = new JObject();
            custom.custom_fields = customFieldsArray;

            dynamic product = new JObject();
            product.key = "pk_live_5066a61cbd1694ec8276b1205f9ebcd885c34701";
            //product.key = "pk_test_9a8895ede03716b9a0474fea6da11ec5bc1c7033";
            product.email = Preferences.Get("userName",string.Empty) == ""? "churchplus@gmail.com" : Preferences.Get("userName", string.Empty);
            product.amount = (mode*100);
            product.subaccount = "ACCT_idvnilipknls9m8";
            //product.bearer = "pk_test_e93ceece8e644d79f5f5b479a194813d6b0f629b";
            product.@ref = id.orderId;
            product.metadata = custom;
            
            hybridWebView.Data = product.ToString();


            //hybridWebView.RegisterCloseAction(() => DisplayAlert("WebView Closed", "Close button clicked", "ok"));
            //hybridWebView.RegisterCallBackAction(data => Confirm());
            //hybridWebView.RegisterCallBackAction(data => ResponsePayment());
        }

       
        private void ResponsePayment()
        {
            vm.PayResponseCommand.Execute(null);
        }

        private void HybridWebView_PaymentClosed(object sender, string e)
        {
           // DisplayAlert("WebView Closed", "Close button clicked event", "ok");
        }


        private void hybridWebView_PaymentSuccessful(object sender, string e)
        {
            //DisplayAlert("Alert", "Payment Reference: " + e, "OK");
            
            vm.PayResponseCommand.Execute(null);
            //await Shell.Current.GoToAsync("../..");
             Confirm();
        }
    }
}