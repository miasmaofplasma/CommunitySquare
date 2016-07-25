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

        string username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.homescreen);
            

            seeMessagesButton = FindViewById<Button>(Resource.Id.b_seeMessage);
            connectBeacon = FindViewById<Button>(Resource.Id.b_connectBeacon);
            userInfoButton = FindViewById<Button>(Resource.Id.b_accountSettings);

            username = Intent.GetStringExtra("_username");

            seeMessagesButton.Click += SeeMessagesButton_Click;
            connectBeacon.Click += ConnectBeaconButton_Click;
            userInfoButton.Click += UserInfoButton_Click;

           

            

            // Create your application here
        }

        private void UserInfoButton_Click(object sender, EventArgs e)
        {
            var userInfo = new Intent(this, typeof(UserOptionsActivity));
            userInfo.PutExtra("_username", username);
            StartActivity(userInfo);
        }

        private void ConnectBeaconButton_Click(object sender, EventArgs e)
        {

            Intent beaconActivity = new Intent(this, typeof(BeaconActivity));
            beaconActivity.PutExtra("_username", username);
            beaconActivity.PutExtra("_boardType", "UserBoard");
            StartActivity(beaconActivity);
        }

        private void SeeMessagesButton_Click(object sender, EventArgs e)
        {

            var seeUserMessageActivity = new Intent(this, typeof(BoardActivity));
            seeUserMessageActivity.PutExtra("_username", username);
            seeUserMessageActivity.PutExtra("_boardType", "UserBoard");
            StartActivity(seeUserMessageActivity);

        }
    }
}