using ChurchMemberApp.Views.Authentication;
using ChurchMemberApp.Views.Media;
using ChurchMemberApp.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChurchMemberApp.Views.Giving
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterAmountPage : ContentPage
    {
        int ucode;
        int currentState = 1;
        public EnterAmountPage()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            med.Opacity = 0;
            await med.FadeTo(1, 500);
            //active.Opacity = 0;
            //await Task.WhenAny<bool>
            //(
            //    active.FadeTo(1, 2000),
            //    active.TranslateTo(0, 0, 2000)
            //);
        }

        //void Button_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    App.Current.MainPage.Navigation.PushAsync(new GiveTowardsPage());
        //}

        //private void home_Tapped(object sender, EventArgs e)
        //{
        //    App.Current.MainPage = new NavigationPage(new FeedPage());
        //}

        //private void media_Tapped(object sender, EventArgs e)
        //{
        //    App.Current.MainPage = new NavigationPage(new MediaPage());
        //}

        //private void profile_Tapped(object sender, EventArgs e)
        //{
        //    App.Current.MainPage = new NavigationPage(new ProfilePage());
        //}

        private void pay_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaymentPage());
        }


        private void OnSelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;


            string pressed = button.Text;
            //validation before button is pressed
            if (this.resultText.Text == "0" || currentState < 0)//at first current state is 1
            {
                this.resultText.Text = "";//here the text value will be cleared when pressing button

                if (currentState < 0) //at first current value is 1 so this condition is excluded
                    currentState *= -1;
            }

            this.resultText.Text += pressed;// this condition is called when current state is greater and text box will aquire the pressed 

            int number;//if  we are going  to assign two dynamic number for a given variable using try parse method 
            if (int.TryParse(this.resultText.Text, out number))
            {
                this.resultText.Text = number.ToString();

                ucode = number;
            }

        }

        private void OnClear(object sender, EventArgs e)
        {
            ucode = 0;
            currentState = 1;
            this.resultText.Text = "0";
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
