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
    [Activity(Label = "MessageActivity")]
    public class MessageActivity : Activity
    {

        string creator;
        string body;
        string title;
        string beaconId;
        string id;
        string createdAt;
        string username;
        string messageType;

        TextView messageTitle;
        TextView messageBody;
        TextView messageCreator;
        TextView messageDate;
        Button replyButton;
        Button seeRepliesButton;
        Button deleteMessage;
        Button returnButton;

        MessageServerAccess db_accesssMessage;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.messageActivity);

            db_accesssMessage = new MessageServerAccess();

            creator = Intent.GetStringExtra("_creator");
            body = Intent.GetStringExtra("_body");
            title = Intent.GetStringExtra("_title");
            beaconId = Intent.GetStringExtra("_beaconId");
            id = Intent.GetStringExtra("_id");
            createdAt = Intent.GetStringExtra("_createdAt");
            username = Intent.GetStringExtra("_username");
            messageType = Intent.GetStringExtra("_messageType") ??  "";


            messageBody = FindViewById<TextView>(Resource.Id.tv_messageBody);
            messageTitle = FindViewById<TextView>(Resource.Id.tv_messageTitle);
            messageCreator = FindViewById<TextView>(Resource.Id.tv_messageCreator);
            messageDate = FindViewById<TextView>(Resource.Id.tv_messageDate);
            replyButton = FindViewById<Button>(Resource.Id.b_replyMessage);
            seeRepliesButton = FindViewById<Button>(Resource.Id.b_seeReplies);
            deleteMessage = FindViewById<Button>(Resource.Id.b_deleteMessage);
            returnButton = FindViewById<Button>(Resource.Id.b_returnBoard);

            deleteMessage.Visibility = ViewStates.Invisible;

            if (messageType.Equals("ReplyMessage"))
            {
                replyButton.Visibility = ViewStates.Invisible;
                seeRepliesButton.Visibility = ViewStates.Invisible;
            }

            messageBody.Text = body;
            messageTitle.Text = title;
            messageCreator.Text = creator;
            messageDate.Text = createdAt;

            replyButton.Click += ReplyButton_Click;
            seeRepliesButton.Click += SeeRepliesButton_Click;
            deleteMessage.Click += DeleteMessage_Click;
            returnButton.Click += ReturnButton_Click;

            if (username.Equals(creator))
            {
                deleteMessage.Visibility = ViewStates.Visible;
            }
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            var returnBoard = new Intent(this, typeof(BoardActivity));
            returnBoard.PutExtra("_beaconID", beaconId);
            returnBoard.PutExtra("_username", username);
            returnBoard.PutExtra("_boardType", "MainBoard");
            StartActivity(returnBoard);
        }

        private void DeleteMessage_Click(object sender, EventArgs e)
        {

            if (messageType.Equals("ReplyMessage"))
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Delete Confirm");
                alert.SetMessage("Are you sure you want to delete this Message?");
                alert.SetPositiveButton("OK", (senderAlert, args) =>
                {
                    db_accesssMessage.deleteReplyMessage(id);
                    var returnBoard = new Intent(this, typeof(BoardActivity));
                    returnBoard.PutExtra("_beaconID", beaconId);
                    returnBoard.PutExtra("_username", username);
                    returnBoard.PutExtra("_boardType", "MainBoard");
                    StartActivity(returnBoard);
                });
                alert.SetNegativeButton("Cancel", (senderAlert, args) => { });
                alert.Show();

            }
            else
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Delete Confirm");
                alert.SetMessage("Are you sure you want to delete this Message? This will also delete reply messages.");
                alert.SetPositiveButton("OK", (senderAlert, args) =>
                {
                    db_accesssMessage.deleteMessage(id);
                    var returnBoard = new Intent(this, typeof(BoardActivity));
                    returnBoard.PutExtra("_beaconID", beaconId);
                    returnBoard.PutExtra("_username", username);
                    returnBoard.PutExtra("_boardType", "MainBoard");
                    StartActivity(returnBoard);
                });
                alert.SetNegativeButton("Cancel", (senderAlert, args) => { });
                alert.Show();
  
            }


        }

        private void SeeRepliesButton_Click(object sender, EventArgs e)
        {
            Intent createReplyMessageActivity = new Intent(this, typeof(BoardActivity));
            createReplyMessageActivity.PutExtra("_beaconID", beaconId);
            createReplyMessageActivity.PutExtra("_parentMessageID", id);
            createReplyMessageActivity.PutExtra("_username", username);
            createReplyMessageActivity.PutExtra("_boardType", "ReplyBoard");
            StartActivity(createReplyMessageActivity);


        }

        private void ReplyButton_Click(object sender, EventArgs e)
        {
            Intent createReplyMessageActivity = new Intent(this, typeof(CreateMessageActivity));
            createReplyMessageActivity.PutExtra("_beaconID", beaconId);
            createReplyMessageActivity.PutExtra("_parentMessageID", id);
            createReplyMessageActivity.PutExtra("_username", username);
            createReplyMessageActivity.PutExtra("_boardType", "MainBoard");
            StartActivity(createReplyMessageActivity);
        }


    }
}