using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CommunitySquare
{
    [Activity(Label = "MessageActivity")]
    public class MessageActivity : ListActivity
    {
        MessageServerAccess db_accesssMessage;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            db_accesssMessage = new MessageServerAccess();

            // Create your application here
        }

        private async void deleteMessage(string messageId)
        {

        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
        }


    }
}