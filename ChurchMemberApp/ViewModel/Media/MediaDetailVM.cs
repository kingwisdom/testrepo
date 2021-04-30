using ChurchMemberApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChurchMemberApp.ViewModel.Media
{
    public class MediaDetailVM : BaseViewModel
    {
        public MediaDetailVM(ChurchMedia model)
        {
            this.ChurchMedia = model;
        }

        private ChurchMedia _church;
        public ChurchMedia ChurchMedia
        {
            get { return _church; }
            set { _church = value; }
        }

        public ICommand PlayCommand => new Command(() =>
        {

        });
    }
}
