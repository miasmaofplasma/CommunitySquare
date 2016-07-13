using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunitySquare
{
    public class ReplyMessage : Message
    {
        public string ParentMessageID { get; set; }
    }
}