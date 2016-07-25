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
    [Activity(Label = "CreateMessageActivity")]
    public class CreateMessageActivity : Activity
    {
        string username;
        string beaconID;
        string typeofBoard;
        string parentMessageID;


        Button createMessageButton;
        Button cancelMessageButton;
        EditText messageTitle;
        EditText messageBody;

        MessageServerAccess db_accessMessage = new MessageServerAccess();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateMessageActivity);

            username = Intent.GetStringExtra("_username") ?? "";
            beaconID = Intent.GetStringExtra("_beaconID") ?? "";
            typeofBoard = Intent.GetStringExtra("_boardType") ?? "";
            parentMessageID = Intent.GetStringExtra("_parentMessageID") ?? "";


            createMessageButton = FindViewById<Button>(Resource.Id.b_messageCreate);
            cancelMessageButton = FindViewById<Button>(Resource.Id.b_messageCancel);

            messageTitle = FindViewById<EditText>(Resource.Id.et_messageTitle);
            messageBody = FindViewById<EditText>(Resource.Id.et_messageBody);

            createMessageButton.Click += CreateMessageButton_Click;
            cancelMessageButton.Click += CancelMessageButton_Click;


            // Create your application here
        }

        private void CancelMessageButton_Click(object sender, EventArgs e)
        {
            Intent backToBoardActivity = new Intent(this, typeof(BoardActivity));
            backToBoardActivity.PutExtra("_username", username);
            backToBoardActivity.PutExtra("_beaconID", beaconID);
            backToBoardActivity.PutExtra("_boardType", typeofBoard);
            StartActivity(backToBoardActivity);
        }

        private void CreateMessageButton_Click(object sender, EventArgs e)
        {
            createMessageButton.Enabled = false;

            if (messageTitle.Text.Equals(""))
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("No Message Title");
                alert.SetMessage("Please enter a title for your message.");
                alert.SetPositiveButton("OK", (senderAlert, args) => { });
                alert.Show();
            }
            else if (messageBody.Text.Equals(""))
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("No Message Body");
                alert.SetMessage("Please enter a body for your message.");
                alert.SetPositiveButton("OK", (senderAlert, args) => { });
                alert.Show();

            }
            else
            {
                if (!username.Equals("") && !beaconID.Equals("") && parentMessageID.Equals(""))
                {
                    db_accessMessage.createMessage(new Message(username, beaconID, messageTitle.Text, messageBody.Text));
                    Intent backToBoardActivity = new Intent(this, typeof(BoardActivity));
                    backToBoardActivity.PutExtra("_username", username);
                    backToBoardActivity.PutExtra("_beaconID", beaconID);
                    backToBoardActivity.PutExtra("_boardType", typeofBoard);
                    StartActivity(backToBoardActivity);
                }
                else if(!username.Equals("") && !beaconID.Equals(""))
                {
                    ReplyMessage reply = new ReplyMessage(username, beaconID, messageTitle.Text, messageBody.Text);
                    reply.ParentMessageID = parentMessageID;
                    createMessageButton.Text = parentMessageID;
                    db_accessMessage.createReplyMessage(reply);
                    Intent backToBoardActivity = new Intent(this, typeof(BoardActivity));
                    createMessageButton.Text = "Create Message";
                    backToBoardActivity.PutExtra("_username", username);
                    backToBoardActivity.PutExtra("_beaconID", beaconID);
                    backToBoardActivity.PutExtra("_boardType", typeofBoard);
                    StartActivity(backToBoardActivity);
                }

            }
            createMessageButton.Enabled = true;
        }
    }
}