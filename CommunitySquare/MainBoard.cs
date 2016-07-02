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
        public List<Message> messageList;


        protected string beaconID;
        protected bool isPrivateBoard { get; set; }
        public string BoardName { get; set; }
        public string BoardId { get; set; }
        public string Creator { get;}
        private string Password { get; set; }


        public MainBoard(string beaconID, string userName)
        {
            this.beaconID = beaconID;
            messageList = new List<Message>();
            isPrivateBoard = false;
            Creator = userName;
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