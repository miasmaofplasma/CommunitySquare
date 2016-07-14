using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunitySquare
{
    public class ReplyBoard : IBoard
    {

        public string ParentMessageID { get;}
        public string BeaconID { get; }

        public ReplyBoard(string beaconID, string parentMsgID)
        {
            this.ParentMessageID = parentMsgID;
            this.BeaconID = beaconID;
        }
    }
}