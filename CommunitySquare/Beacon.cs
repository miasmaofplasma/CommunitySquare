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
using EstimoteSdk;

namespace CommunitySquare
{
    [Activity(Label = "Beacon")]
    public class Beacon : Activity, BeaconManager.IServiceReadyCallback
    {
        BeaconManager _beaconManager;
        EstimoteSdk.Region region;
        private string scanId;
        TextView text;
        const string BeaconId = "com.refactored";
        bool isScanning = false;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.beacons);

            _beaconManager = new BeaconManager(this);
            _beaconManager.Connect(this);

             text = FindViewById<TextView>(Resource.Id.beaconId);

            _beaconManager.Nearable += _beaconManager_Nearable;

            // Create your application here
        }

        private void _beaconManager_Nearable(object sender, BeaconManager.NearableEventArgs e)
        {
            if (e.Nearables.Count > 0)
                text.Text = e.Nearables.FirstOrDefault().BatteryLevel.ToString();
            else text.Text = "No nearable " + scanId;
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (!isScanning) return;
            isScanning = false;
            _beaconManager.StopNearableDiscovery(scanId);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _beaconManager.Disconnect();
        }

        public void OnServiceReady()
        {
            isScanning = true;
            scanId = _beaconManager.StartNearableDiscovery();
        }
    }
}