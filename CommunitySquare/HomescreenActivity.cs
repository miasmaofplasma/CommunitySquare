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
    [Activity(Label = "Homescreen")]
    public class HomescreenActivity : Activity
    {
        Button seeMessagesButton;
        Button connectBeacon;
        Button userInfoButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.homescreen);

            seeMessagesButton = FindViewById<Button>(Resource.Id.b_seeMessage);
            connectBeacon = FindViewById<Button>(Resource.Id.b_connectBeacon);
            userInfoButton = FindViewById<Button>(Resource.Id.b_accountSettings);

            seeMessagesButton.Click += SeeMessagesButton_Click;
            connectBeacon.Click += ConnectBeaconButton_Click;
            userInfoButton.Click += UserInfoButton_Click;

           

            

            // Create your application here
        }

        private void UserInfoButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ConnectBeaconButton_Click(object sender, EventArgs e)
        {
            string username = Intent.GetStringExtra("_username");
            Intent beaconActivity = new Intent(this, typeof(BeaconActivity));
            beaconActivity.PutExtra("_username", username);
            StartActivity(beaconActivity);
        }

        private void SeeMessagesButton_Click(object sender, EventArgs e)
        {

            StartActivity(typeof(BoardActivity));
        }
    }
}