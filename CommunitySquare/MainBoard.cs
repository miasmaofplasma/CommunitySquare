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
    public class MainBoard : IBoard
    {

        [Newtonsoft.Json.JsonProperty("Id")]
        public string id { get; set; }
        public string beaconID { get; set; }
        public bool isPrivateBoard { get; set; }
        public string BoardName { get; set; }
        public string Creator { get; set; }
        public string Password { get; set; }



        public MainBoard(string beaconID, string userName, string boardName)
        {
            this.beaconID = beaconID;
            this.BoardName = boardName;
            Password = "";
            isPrivateBoard = false;
            Creator = userName;
        }
    }
}