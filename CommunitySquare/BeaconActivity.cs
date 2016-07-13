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
using Java.Util;

namespace CommunitySquare
{
    [Activity(Label = "Beacon")]
    public class BeaconActivity : Activity, BeaconManager.IServiceReadyCallback
    {
        BeaconManager _beaconManager;
        static int count = 0;
        private string scanId;
        TextView beaconStatus;
        const string BeaconId = "com.refactored";
        bool isScanning = false;
        ArrayAdapter adapter;
        Dictionary<string, string> beaconsList;
        private int dropCount;


        private BoardServerAccess db_accessBoard;
        string[] beaconData = new string[0];
        List<BeaconView> myBeacons = new List<BeaconView>();
        ListView viewList;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.beacons);
            viewList = FindViewById<ListView>(Resource.Id.beaconList);
            beaconsList = new Dictionary<string, string>();

            db_accessBoard = new BoardServerAccess();

            beaconStatus = FindViewById<TextView>(Resource.Id.beaconId);
            beaconStatus.Text = "Finding beacons...";

            viewList.ItemClick += ViewList_ItemClick;
            // Create your application here



        }



        private async void ViewList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var viewList = sender as ListView;
            var t = myBeacons[e.Position];
            if(t.BoardName.Equals("Not Found"))
            {
                await db_accessBoard.createNewBoard(t.BeaconId, new MainBoard(t.BeaconId, "Ryan", "TestBoard"));
            }
            else
            {
                var boardActivity = new Intent(this, typeof(BoardActivity));
                boardActivity.PutExtra("_boardType", "MainBoard");
                boardActivity.PutExtra("_beaconID", t.BeaconId);
                StartActivity(boardActivity);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            _beaconManager = new BeaconManager(this);
            _beaconManager.Connect(this);
            _beaconManager.Nearable += _beaconManager_Nearable;
            _beaconManager.Nearable += UpdateBoardNames;
            myBeacons = new List<BeaconView>();
            viewList.Adapter = new BeaconAdapter(this, myBeacons);

        }

        private void _beaconManager_Nearable(object sender, BeaconManager.NearableEventArgs e)
        {
            
            if (e.Nearables.Count > 0)
            {
                myBeacons = new List<BeaconView>();
                IList<Nearable> beacons = e.Nearables;

                for (int i = 0; i < e.Nearables.Count; i++)
                {
                    myBeacons.Add(new BeaconView(beacons[i].Identifier));
                }
                beaconStatus.Text = "";
                dropCount = 0;
            }
            else if(e.Nearables.Count == 0 && dropCount<5 && myBeacons.Count > 0)
            {
                dropCount++;
            }
            else
            {
                beaconStatus.Text = "No beacons in range";
                myBeacons = new List<BeaconView>();
                dropCount = 0;
            }

        }

        private async void UpdateBoardNames(object sender, BeaconManager.NearableEventArgs e)
        {
            for (int i = 0; i < myBeacons.Count; i++)
            {
                try
                {
                    beaconStatus.Text = "Loading Beacons";
                    BeaconView beaconView = myBeacons[i];
                    string boardname = "Not Found";
                    if (beaconsList.ContainsKey(beaconView.BeaconId))
                    {
                        beaconsList.TryGetValue(beaconView.BeaconId, out boardname);
                        myBeacons[i].BoardName = boardname;
                    }

                    else
                    {
                        MainBoard mb = await db_accessBoard.returnMainBoard(beaconView.BeaconId);
                        if (mb != null)
                        {
                            myBeacons[i].BoardName = mb.BoardName;

                            if (!beaconsList.ContainsKey(myBeacons[i].BeaconId))
                            {
                                beaconsList.Add(myBeacons[i].BeaconId, myBeacons[i].BoardName);
                            }
                        }
                    }
                    beaconStatus.Text = "";
                }
                catch (System.Exception ex)
                {
                    if(ex is System.Net.WebException)
                    {
                        beaconStatus.Text = "Web error";
                    }
                }

            }
            viewList.Adapter = new BeaconAdapter(this, myBeacons);
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