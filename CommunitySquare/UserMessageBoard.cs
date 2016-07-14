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
    }
}