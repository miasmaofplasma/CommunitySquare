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
    public class LoginServerAccess
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://comsquare.azurewebsites.net");


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
            if(userExist == false)
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

        public bool VerifyCredentials(string UNorPW)
        {
            if (UNorPW.Length < 3 || !Char.IsLetter(UNorPW.ToCharArray()[0]))
            {
                return false;
            }
            else return true;
        }



    }

}