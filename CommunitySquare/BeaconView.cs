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
    public class BeaconView
    {
        public string BoardName { get; set; }
        public string BeaconId { get; set; }

        public BeaconView(string id)
        {
            this.BoardName = "Not Found";
            this.BeaconId = id;
        }
    }
}