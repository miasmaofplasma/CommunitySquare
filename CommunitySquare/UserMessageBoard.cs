using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunitySquare
{
    public class UserMessageBoard : IBoard
    {
        private List<Message> userMessages = new List<Message>();
        protected string userID { get; }

        public UserMessageBoard(string userId)
        {
            this.userID = userID;
        }

        public void addMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public List<Message> getMessages()
        {
            throw new NotImplementedException();
        }

        public string[] getMessageTitles()
        {
            throw new NotImplementedException();
        }
    }
}