using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunitySquare
{
    public class ReplyMessage : Message
    {
        public ReplyMessage(string creator, string beaconID, string title, string Body) : base(creator, beaconID, title, Body)
        {

        }

        public string ParentMessageID { get; set; }

        public Message ReturnBase()
        {
            return (Message) this;
        }

    }
}