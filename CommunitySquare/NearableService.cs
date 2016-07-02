using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using EstimoteSdk;

namespace CommunitySquare
{
    [Service]
    public class NearableService : Service
    {
        BeaconManager _beaconManager;

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}