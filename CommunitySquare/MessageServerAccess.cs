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
    public class MessageServerAccess :ServerAccessAbstract
    {
        private MobileServiceClient MobileClient;

        public MessageServerAccess()
            {
                MobileClient = new MobileServiceClient("https://comsquare.azurewebsites.net");
            }

        public async Task<List<Message>> getUserMessages(string username)
        {
            List<Message> messageList = await MobileClient.GetTable<Message>().Where
                 (p => p.Creator == username)
                 .ToListAsync();

            return messageList;
        }

        public async Task<List<Message>> getMainBoardMessages(string BeaconID)
        {
            List <Message> messageList =  await MobileClient.GetTable<Message>().Where
                (p => p.BeaconID == BeaconID)
                .ToListAsync();

            return messageList;
        }

        public async Task<List<ReplyMessage>> getReplyMessages(string messageId)
        {
            List<ReplyMessage> replyMessageList = await MobileClient.GetTable<ReplyMessage>().Where
                (p => p.ParentMessageID == messageId)
                .ToListAsync();


            return replyMessageList;
        }

        public async void createMessage(Message message)
        {
            await MobileClient.GetTable<Message>().InsertAsync(message);
        }

        public async void createReplyMessage(ReplyMessage reply)
        {
            await MobileClient.GetTable<ReplyMessage>().InsertAsync(reply);
        }

        public async void deleteMessage(string messageId)
        {
            List<Message> messageList = await MobileClient.GetTable<Message>().Where(p => p.id == messageId).ToListAsync();

            foreach(Message mes in messageList)
            {
                List<ReplyMessage> replyList = await MobileClient.GetTable<ReplyMessage>().Where(p => p.ParentMessageID == mes.id).ToListAsync();
                {
                    foreach(ReplyMessage rmes in replyList)
                    {
                        await MobileClient.GetTable<ReplyMessage>().DeleteAsync(rmes);
                    }
                    await MobileClient.GetTable<Message>().DeleteAsync(mes);
                }
                
            }
        }

        public async void deleteReplyMessage(string messageID)
        {
            List<ReplyMessage> replyList = await MobileClient.GetTable<ReplyMessage>().Where(p => p.id == messageID).ToListAsync();
            foreach(ReplyMessage rmes in replyList)
            {
                await MobileClient.GetTable<ReplyMessage>().DeleteAsync(rmes);
            }
        }


    }



}