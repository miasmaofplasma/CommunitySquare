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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.homescreen);

            var seeMessages = FindViewById<Button>(Resource.Id.b_seeMessage);
            var connectBeaacon = FindViewById<Button>(Resource.Id.b_connectBeacon);

            seeMessages.Click += delegate
            {
                StartActivity(typeof(BoardActivity));
            };

            connectBeaacon.Click += delegate
            {
                StartActivity(typeof(Beacon));
            };
            
            

            // Create your application here
        }
    }
}