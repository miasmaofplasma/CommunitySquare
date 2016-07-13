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

    public enum boardType
    {
        mainBoard,
        replyBoard,
        userMessageBoard,
    }

    [Activity(Label = "BoardActivity")]
    public class BoardActivity : Activity
    {
        BoardServerAccess db_accessBoard;
        MessageServerAccess db_accessMessage;
        private string typeOfBoard;

        IBoard messageBoard;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BoardActivity);
            
            db_accessBoard = new BoardServerAccess();
            db_accessMessage = new MessageServerAccess();

            typeOfBoard = Intent.GetStringExtra("_boardType") ?? "";

            var boardStatus = FindViewById<TextView>(Resource.Id.BoardStatus);
            boardStatus.Text = typeOfBoard;

            if (typeOfBoard.Equals("MainBoard"))
            {

            }

            


            

            // Create your application here
        }
    }
}