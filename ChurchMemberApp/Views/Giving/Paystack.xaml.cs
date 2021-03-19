using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Services;
using ChurchMemberApp.ViewModel.Giving;
using Newtonsoft.Json.Linq;
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
            Init(model, id);
            vm = new ContributionItemVM(id);
        }

        private void Init(decimal mode, PaymentForm id)
        {
            loading.IsVisible = false;

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
            product.key = "pk_test_9a8895ede03716b9a0474fea6da11ec5bc1c7033";
            product.email = Preferences.Get("userName",string.Empty) == ""? "churchplus@gmail.com" : Preferences.Get("userName", string.Empty);
            product.amount = (mode*100);
            //product.subaccount = "ACCT_wui3t98msbutpca";
            //product.bearer = "pk_test_e93ceece8e644d79f5f5b479a194813d6b0f629b";
            product.@ref = id.orderId;
            product.metadata = custom;
            
            hybridWebView.Data = product.ToString();


            hybridWebView.RegisterCloseAction(() => DisplayAlert("WebView Closed", "Close button clicked", "ok"));
            hybridWebView.RegisterCallBackAction(data => DisplayAlert("Alert", "Hello " + data, "OK"));
            //hybridWebView.RegisterCallBackAction(data => ResponsePayment());
        }

        private void ResponsePayment()
        {
            vm.PayResponseCommand.Execute(null);
        }

        private void HybridWebView_PaymentClosed(object sender, string e)
        {
            DisplayAlert("WebView Closed", "Close button clicked event", "ok");
        }


        private async void hybridWebView_PaymentSuccessful(object sender, string e)
        {
            //DisplayAlert("Alert", "Payment Reference: " + e, "OK");
            
            vm.PayResponseCommand.Execute(null);
            await Shell.Current.GoToAsync("../..");
        }
    }
}