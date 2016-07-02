using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunitySquare
{
    public interface IBoard
    {
        string[] getMessageTitles();
        List<Message> getMessages();
        void addMessage(Message message);
    }
}