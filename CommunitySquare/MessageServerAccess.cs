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
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace CommunitySquare
{
    public class MessageServerAccess : ServerAccessAbstract
    {
        private MobileServiceClient MobileClient;

        public MessageServerAccess()
            {
                MobileClient = base.MobileService;
            }

        public async Task<List<Message>> getUserMessages(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Message>> getMainBoardMessages(string BoardId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> getReplyMessages(string messageId)
        {
            throw new NotImplementedException();
        }

        public void deleteMessage(string messageId)
        {

        }
    }



}