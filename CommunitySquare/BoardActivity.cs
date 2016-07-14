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
using System.Threading.Tasks;

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
        private List<Message> messageList;
        ListView messageListview;
        TextView boardStatus;
        Button createMessageButton;
        string username;
        string beaconId;

        IBoard messageBoard;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BoardActivity);

            db_accessBoard = new BoardServerAccess();
            db_accessMessage = new MessageServerAccess();

            beaconId = Intent.GetStringExtra("_beaconID") ?? "";
            username = Intent.GetStringExtra("_username") ?? "";
            typeOfBoard = Intent.GetStringExtra("_boardType") ?? "";

            createMessageButton = FindViewById<Button>(Resource.Id.b_boardCreateMessage);
            createMessageButton.Click += CreateMessageButton_Click;

            messageList = new List<Message>();
            messageListview = FindViewById<ListView>(Resource.Id.messageListview);
            messageListview.Adapter = new MessageAdapter(this, messageList);

            // Create your application here
        }

        private void CreateMessageButton_Click(object sender, EventArgs e)
        {
            Intent createMessageActivity = new Intent(this, typeof(CreateMessageActivity));
            createMessageActivity.PutExtra("_username", username);
            createMessageActivity.PutExtra("_beaconID", beaconId);
            createMessageActivity.PutExtra("_boardType", typeOfBoard);

            StartActivity(createMessageActivity);
        }

        protected async override void OnStart()
        {
            base.OnStart();
            messageBoard = await loadBoards(typeOfBoard);
            
            if (typeOfBoard.Equals("MainBoard"))
            {
                string beaconId = Intent.GetStringExtra("_beaconID") ?? "";

                
                if(!beaconId.Equals(""))
                {
                    messageList = await db_accessMessage.getMainBoardMessages(beaconId);
                }

                messageListview.Adapter =  new MessageAdapter(this, messageList);
            }
                                                                                    
        }

        protected async Task<IBoard> loadBoards(string mainBoard)
        {
            if (typeOfBoard.Equals("MainBoard"))
            {
                string beaconID = Intent.GetStringExtra("_beaconID") ?? "";
                if (beaconID.Equals(""))
                {
                    return null;
                }
                MainBoard mb = await db_accessBoard.returnMainBoard(beaconID);
                return mb;
                
            }
            return null;
        }
    }
}