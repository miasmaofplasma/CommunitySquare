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
    [Activity(Label = "UserOptionsActivity")]
    public class UserOptionsActivity : Activity
    {
        CheckBox sendNotificationsCheckbox;
        Button deleteAccountButton;
        Button logoutButton;
        LoginServerAccess db_accessServer;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            db_accessServer = new LoginServerAccess();
            deleteAccountButton.Click += DeleteAccountButton_Click;
            logoutButton.Click += LogoutButton_Click;
            sendNotificationsCheckbox.Click += SendNotificationsCheckbox_Click;
            // Create your application here
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SendNotificationsCheckbox_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void storeNotificationSetting()
        {

        }

        private string loadNotificaitonSetting()
        {
            throw new NotImplementedException();
        }
    }
}