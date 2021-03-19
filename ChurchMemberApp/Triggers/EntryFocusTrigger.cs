using System;
using Xamarin.Forms;

namespace ChurchMemberApp.Triggers
{
    public class EntryFocusTrigger : TriggerAction<BoxView>
    {
        protected override void Invoke(BoxView sender)
        {
            sender.BackgroundColor = Color.FromHex("#140F26");
        }
    }
}
