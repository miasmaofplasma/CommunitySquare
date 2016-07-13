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
            List<MainBoard> boardList = await MobileService.GetTable<MainBoard>()
                .Where(p => p.beaconID == beaconID)
                .ToListAsync();
            if (boardList.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<MainBoard> returnMainBoard(string beaconID)
        {
            List<MainBoard> boardList = await MobileService.GetTable<MainBoard>()
            .Where(p => p.beaconID == beaconID)
            .ToListAsync();

            if (boardList.Count == 1)
            {
                MainBoard returnBoard = new MainBoard(boardList[0].beaconID, boardList[0].Creator, boardList[0].BoardName);
                if(boardList[0].isPrivateBoard)
                {
                    returnBoard.Password = boardList[0].Password;
                    returnBoard.isPrivateBoard = boardList[0].isPrivateBoard;
                }
                return returnBoard;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> createNewBoard(string beaconId, MainBoard board)
        {
            bool boardExists = await checkBoardCreated(board.beaconID);
            if(boardExists == false)
            {
                await MobileService.GetTable<MainBoard>().InsertAsync(board);
                return true;
            }
            return false;

        }

        public async void deleteBoard(string beaconId)
        {
            throw new NotImplementedException();
        }
    }
}