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
    public class BoardServerAccess : ServerAccessAbstract
    {
        private new MobileServiceClient MobileService;

        public BoardServerAccess()
            {
                this.MobileService = base.MobileService;
            }

        public async Task<bool> checkBoardCreated(string beaconID)
        {
            throw new NotImplementedException();
        }

        public async Task<MainBoard> returnMainBoard(string beaconID)
        {
            throw new NotImplementedException();
        }

        public async void createNewBoard(string beaconId, MainBoard board)
        {
            throw new NotImplementedException();
        }


    }
}