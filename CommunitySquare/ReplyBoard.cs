using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunitySquare
{
    public class ReplyBoard : IBoard
    {
        protected string ParentMessageID { get;}
        protected string BeaconID { get; }

        private List<Message> messageList;

        public ReplyBoard(string beaconID, string parentMsgID)
        {
            this.messageList = new List<Message>();
            this.ParentMessageID = parentMsgID;
            this.BeaconID = beaconID;
        }

        public string[] getMessageTitles()
        {
            throw new NotImplementedException();
        }

        public List<Message> getMessages()
        {
            throw new NotImplementedException();
        }

        public void addMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}