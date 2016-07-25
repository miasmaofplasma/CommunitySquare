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
    public class LoginServerAccess : ServerAccessAbstract
    {
        protected MobileServiceClient MobileService;

        public LoginServerAccess()
        {
            this.MobileService = base.MobileService;
        }

        public async Task<UserInfo> AtteptLogin(string username, string password)
        {
            List<UserInfo> loginList = await MobileService.GetTable<UserInfo>()
                .Where(p => p.UserName == username)
                .Where(p => p.Password == password)
                .ToListAsync();

            if (loginList.Count == 1)
            {
                return new UserInfo { UserName = loginList[0].UserName, Password = loginList[0].Password, ID = loginList[0].ID };

            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CheckUsername(string username, string password)
        {
            List<UserInfo> loginList = await MobileService.GetTable<UserInfo>()
                .Where(p => p.UserName == username)
                .ToListAsync();

            if (loginList.Count == 1)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        public async Task<bool> CreateAccount(string username, string password)
        {
            bool userExist = await CheckUsername(username, password);
            if (userExist == false)
            {
                await MobileService.GetTable<UserInfo>().InsertAsync(new UserInfo { UserName = username, Password = password });
                UserInfo user = await AtteptLogin(username, password);
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public async Task<UserInfo> ReturnUser(String username)
        {
            List<UserInfo> loginList = await MobileService.GetTable<UserInfo>()
            .Where(p => p.UserName == username)
            .ToListAsync();

            if (loginList.Count == 1)
            {
                return new UserInfo { UserName = loginList[0].UserName, Password = loginList[0].Password, ID = loginList[0].ID };

            }
            else
            {
                return null;
            }
        }

        public bool VerifyCredentials(string UNorPW)
        {
            if (UNorPW.Length < 3 || !Char.IsLetter(UNorPW.ToCharArray()[0]))
            {
                return false;
            }
            else return true;
        }

        public async void DeleteAcount(UserInfo user)
        {
            BoardServerAccess db_boardAccess = new BoardServerAccess();
            MessageServerAccess db_messageAccess = new MessageServerAccess();

           List<MainBoard> boardList = await MobileService.GetTable<MainBoard>().Where(p => p.Creator == user.UserName).ToListAsync();
            foreach(MainBoard mb in boardList)
            {
                db_boardAccess.deleteBoard(mb.beaconID);
            }

            List<Message> messageList = await MobileService.GetTable<Message>().Where(p => p.Creator == user.UserName).ToListAsync();
            foreach(Message mes in messageList)
            {
                db_messageAccess.deleteMessage(mes.id);
            }
            List<ReplyMessage> replyList = await MobileService.GetTable<ReplyMessage>().Where(p => p.Creator == user.UserName).ToListAsync();
            foreach(ReplyMessage rmes in replyList)
            {
                db_messageAccess.deleteReplyMessage(rmes.id);
            }

            await MobileService.GetTable<UserInfo>().DeleteAsync(user);

        }


    }

}