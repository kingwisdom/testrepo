using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using ChurchMemberApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Giving
{
    public class OfferingFormViewModel : BaseViewModel
    {
        Imessaging toast;
        public OfferingFormViewModel()
        {
            Oft = new ObservableCollection<PaymentForm>();
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
            set { _oft = value; }
        }

        private ObservableCollection<ContributionItem> citem;
        public ObservableCollection<ContributionItem> ContributionItems
        {
            get { return citem; }
            set { citem = value; }
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
                foreach (var item in res)
                {
                    Oft.Add(item);
                 
                }
                
            }
            catch (Exception ex)
            {
                toast.LongAlert(ex.Message);
                IsBusy = false;
            }
        });

    }
}
