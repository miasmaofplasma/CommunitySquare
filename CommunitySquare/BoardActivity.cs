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
using System.Threading;

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
        private List<ReplyMessage> replyMessageList;
        ListView messageListview;
        Button createMessageButton;
        Button boardRefreshButton;
        Button delteBoardButton;
        string username;
        string beaconId;
        string parentMessageID;
        string creator;
        string boardCreator;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BoardActivity);



            beaconId = Intent.GetStringExtra("_beaconID") ?? "";
            username = Intent.GetStringExtra("_username") ?? "";
            typeOfBoard = Intent.GetStringExtra("_boardType") ?? "";
            parentMessageID = Intent.GetStringExtra("_parentMessageID") ?? "";



            createMessageButton = FindViewById<Button>(Resource.Id.b_boardCreateMessage);
            createMessageButton.Click += CreateMessageButton_Click;

            boardRefreshButton = FindViewById<Button>(Resource.Id.b_boardRefresh);
            boardRefreshButton.Click += BoardRefreshButton_Click;

            delteBoardButton = FindViewById<Button>(Resource.Id.b_deleteBoard);
            delteBoardButton.Click += DelteBoardButton_Click;

            messageList = new List<Message>();
            replyMessageList = new List<ReplyMessage>();
            messageListview = FindViewById<ListView>(Resource.Id.messageListview);
            messageListview.Adapter = new MessageAdapter(this, messageList);

            messageListview.ItemClick += MessageListview_ItemClick;

            delteBoardButton.Visibility = ViewStates.Invisible;
            createMessageButton.Visibility = ViewStates.Invisible;



            // Create your application here
        }

        protected async override void OnStart()
        {

            base.OnStart();
            db_accessBoard = new BoardServerAccess();
            db_accessMessage = new MessageServerAccess();

            if (typeOfBoard.Equals("MainBoard"))
            {
                createMessageButton.Visibility = ViewStates.Visible;
                if (!beaconId.Equals(""))
                {
                    messageList = await db_accessMessage.getMainBoardMessages(beaconId);
                }
                MainBoard mb = await db_accessBoard.returnMainBoard(beaconId);
                boardCreator = mb.Creator;

                if (boardCreator.Equals(username))
                {
                    delteBoardButton.Visibility = ViewStates.Visible;
                }


                messageListview.Adapter = new MessageAdapter(this, messageList);
            }


            if (typeOfBoard.Equals("ReplyBoard"))
            {
                messageList = new List<Message>();
                if (!parentMessageID.Equals(""))
                {
                    replyMessageList = await db_accessMessage.getReplyMessages(parentMessageID);
                }

                foreach (ReplyMessage rm in replyMessageList)
                {
                    messageList.Add(rm.ReturnBase());
                }

                messageListview.Adapter = new MessageAdapter(this, messageList);
            }

            if (typeOfBoard.Equals("UserBoard"))
            {
                messageList = new List<Message>();
                {
                    messageList = await db_accessMessage.getUserMessages(username);
                }

                messageListview.Adapter = new MessageAdapter(this, messageList);
            }

        }

        private async void DelteBoardButton_Click(object sender, EventArgs e)
        {
            var alert = new AlertDialog.Builder(this);
            alert.SetTitle("Delete Confirm");
            alert.SetMessage("Are you sure you want to delete this board? It will delete all messages associated with this board as well.");
            alert.SetPositiveButton("OK", async (senderAlert, args) =>
            {
                await db_accessBoard.deleteBoard(beaconId);
                var backToHome = new Intent(this, typeof(HomescreenActivity));
                backToHome.PutExtra("_username", username);
                StartActivity(backToHome);
            });
            alert.SetNegativeButton("Cancel", (senderAlert, args) => { });
            alert.Show();





        }

        private void MessageListview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var viewList = sender as ListView;
            var t = messageList[e.Position];

            string creator = t.Creator;
            string body = t.Body;
            string beaconId = t.BeaconID;
            string title = t.Title;
            string id = t.id;
            string createdAt = t.createdAt;

            Intent messageActivity = new Intent(this, typeof(MessageActivity));
            messageActivity.PutExtra("_username", username);
            messageActivity.PutExtra("_creator", creator);
            messageActivity.PutExtra("_body", body);
            messageActivity.PutExtra("_beaconId", beaconId);
            messageActivity.PutExtra("_title", title);
            messageActivity.PutExtra("_id", id);
            messageActivity.PutExtra("_createdAt", createdAt);
            if (typeOfBoard.Equals("MainBaord") || typeOfBoard.Equals("UserBoard"))
            {
                messageActivity.PutExtra("_messageType", "Message");
            }
            else if (typeOfBoard.Equals("ReplyBoard"))
            {
                messageActivity.PutExtra("_messageType", "ReplyMessage");
            }

            StartActivity(messageActivity);
        }

        private async void BoardRefreshButton_Click(object sender, EventArgs e)
        {
            messageList = new List<Message>();
            messageListview.Adapter = new MessageAdapter(this, messageList);

            if (typeOfBoard.Equals("MainBoard"))
            {
                string beaconId = Intent.GetStringExtra("_beaconID") ?? "";


                if (!beaconId.Equals(""))
                {
                    messageList = await db_accessMessage.getMainBoardMessages(beaconId);
                }

                messageListview.Adapter = new MessageAdapter(this, messageList);
            }
            else if (typeOfBoard.Equals("ReplyBoard"))
            {
                messageList = new List<Message>();
                if (!parentMessageID.Equals(""))
                {
                    replyMessageList = await db_accessMessage.getReplyMessages(parentMessageID);
                }

                foreach (ReplyMessage rm in replyMessageList)
                {
                    messageList.Add(rm.ReturnBase());
                }

                messageListview.Adapter = new MessageAdapter(this, messageList);
            }
            else if (typeOfBoard.Equals("UserBoard"))
            {

                messageList = new List<Message>();
                {
                    messageList = await db_accessMessage.getUserMessages(username);
                }

                messageListview.Adapter = new MessageAdapter(this, messageList);
            }
        }

        private void CreateMessageButton_Click(object sender, EventArgs e)
        {
            if (typeOfBoard.Equals("MainBoard"))
            {
                Intent createMessageActivity = new Intent(this, typeof(CreateMessageActivity));
                createMessageActivity.PutExtra("_username", username);
                createMessageActivity.PutExtra("_beaconID", beaconId);
                createMessageActivity.PutExtra("_boardType", typeOfBoard);
                StartActivity(createMessageActivity);
            }

        }



    }
}