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
using Android.Text;

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
        static int webCount = 0;


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
            try
            {
                var viewList = sender as ListView;
                var t = myBeacons[e.Position];


                if (t.BoardName.Equals("Not Found"))
                {
                    Intent createBoardActivity = new Intent(this, typeof(CreateBoardActivity));
                    string username = Intent.GetStringExtra("_username");
                    createBoardActivity.PutExtra("_beaconID", t.BeaconId);
                    createBoardActivity.PutExtra("_username", username);
                    StartActivity(createBoardActivity);
                }
                else
                {
                    MainBoard mb = await db_accessBoard.returnMainBoard(t.BeaconId);
                    var boardActivity = new Intent(this, typeof(BoardActivity));
                    string username = Intent.GetStringExtra("_username");
                    boardActivity.PutExtra("_boardType", "MainBoard");
                    boardActivity.PutExtra("_beaconID", t.BeaconId);
                    boardActivity.PutExtra("_username", username);


                    if (mb.isPrivateBoard)
                    {
                        webCount = 0;
                        EditText input = new EditText(this);
                        var alert = new AlertDialog.Builder(this);
                        input.SetRawInputType(InputTypes.TextVariationPassword);
                        alert.SetView(input);
                        alert.SetTitle("Private Board");
                        alert.SetMessage("Board is private, plase enter the password.");
                        alert.SetPositiveButton("OK", (senderAlert, args) =>
                        {
                            if (mb.Password.Equals(input.Text))
                            {
                                StartActivity(boardActivity);
                            }
                            else
                            {
                                var alert2 = new AlertDialog.Builder(this);
                                alert2.SetTitle("Private Board");
                                alert2.SetMessage("Password Incorrect");
                                alert2.SetPositiveButton("OK", (senderAlert2, args2) => { });
                                alert2.Show();
                            }

                        });
                        alert.SetNegativeButton("Cancel", (senderAlert, args) => { });
                        alert.Show();

                    }
                    else
                    {
                        webCount = 0;
                        StartActivity(boardActivity);
                    }

                }
            }
            catch(Exception ex)
            {
                if (ex is System.Net.WebException && webCount<3)
                {
                    ViewList_ItemClick(sender, e);
                }
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
            else if(e.Nearables.Count < myBeacons.Count && dropCount<5 && myBeacons.Count > 0)
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
                    //bool boardCreated = await db_accessBoard.createNewBoard(beaconView.BeaconId, new MainBoard(beaconView.BeaconId, "Ryan Barrett", "Test Board 1"));
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