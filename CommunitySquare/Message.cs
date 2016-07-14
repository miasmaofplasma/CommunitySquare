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
    public class Message
    {
        public string Creator { get; set; }
        public string BeaconID { get; set; }
        public string Title { get; set;}
        public string Body { get; set; }

        [Newtonsoft.Json.JsonProperty("Id")]
        public string id { get; set; }

        public Message(string creator, string beaconID, string title, string Body)
        {
            this.Creator = creator;
            this.BeaconID = beaconID;
            this.Title = title;
            this.Body = Body;
        }
    }
}