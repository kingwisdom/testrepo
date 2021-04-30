using ChurchMemberApp.Services;
using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ChurchMemberApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrcodeScanPage : ZXingScannerPage
    {
        public QrcodeScanPage()
        {
            InitializeComponent();
        }

        public ZXingScannerView scanner = new ZXingScannerView();

        [Obsolete]
        private async void scanView_OnScanResult(ZXing.Result result)
        {

            scanner.Options = new MobileBarcodeScanningOptions()
            {
                UseFrontCameraIfAvailable = false, //update later to come from settings
                PossibleFormats = new List<BarcodeFormat>(),
                TryHarder = true,
                AutoRotate = false,
                TryInverted = true,
                DelayBetweenContinuousScans = 2000
            };

            scanner.IsVisible = false;
            scanner.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);
            scanner.Options.PossibleFormats.Add(BarcodeFormat.DATA_MATRIX);
            scanner.Options.PossibleFormats.Add(BarcodeFormat.EAN_13);

            if (scanner.IsScanning)
            {
                scanner.AutoFocus();
            }

            try
            {
                if (string.IsNullOrEmpty(Preferences.Get(App.USERSIGNEDINKEY, string.Empty)))
                {
                    await Navigation.PushModalAsync(new LoginPage());
                    return;
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var res = await ApiServices.MarkAttendance(Preferences.Get("userId", string.Empty), result.Text);

                    if (res == null)
                    {

                        //await DisplayAlert("", "You have already marked the attendance", "Alright");
                        MessageDialog.Show("","Your record is not fond in this church","Cancel");
                    }
                    else
                    {
                        if (res.status)
                        {
                            await PopupNavigation.PushAsync(new MarkPopup());

                            await PopupNavigation.PopAsync();
                            // await DisplayAlert("", res.message, "Alright");
                        }
                        else
                        {
                            await DisplayAlert("", res.message, "Cancel");
                        }
                    }

                    await Navigation.PopAsync();
                    // await DisplayAlert("Scanned result", "The barcode's text is " + result.Text + ". The barcode's format is " + result.BarcodeFormat, "OK");

                });

            }
            catch
            {
                await DisplayAlert("", "You have already marked the attendance", "Alright");

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IsScanning = false;
        }

    }
}