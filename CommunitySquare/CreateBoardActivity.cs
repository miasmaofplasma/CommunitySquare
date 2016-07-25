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
using Android.Text;

namespace CommunitySquare
{
    [Activity(Label = "CreateBoardActivity")]
    public class CreateBoardActivity : Activity
    {
        BoardServerAccess db_accessBoard;
        EditText boardTitle;
        EditText boardPassword;
        Button createBoardButton;
        Button cancelButton;
        CheckBox isPrivateCheckbox;
        TextView passwordText;

        string username;
        string beaconId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.createBoard);

            username = Intent.GetStringExtra("_username");
            beaconId = Intent.GetStringExtra("_beaconID");

            db_accessBoard = new BoardServerAccess();
            createBoardButton = FindViewById<Button>(Resource.Id.b_boardCreateOK);
            cancelButton = FindViewById<Button>(Resource.Id.b_boardCreateCancel);
            isPrivateCheckbox = FindViewById<CheckBox>(Resource.Id.cb_boardCreateIsProtected);
            boardPassword = FindViewById<EditText>(Resource.Id.et_boardCreatePassword);
            boardTitle = FindViewById<EditText>(Resource.Id.et_boardCreateTitle);
            passwordText = FindViewById<TextView>(Resource.Id.tv_createBoardPassword);

            boardPassword.Visibility = ViewStates.Invisible;
            passwordText.Visibility = ViewStates.Invisible;

            isPrivateCheckbox.Click += IsPrivateCheckbox_Click;
            //cancelButton.Click += CancelButton_Click;
            createBoardButton.Click += CreateBoardButton_Click;
        }

        private async void CreateBoardButton_Click(object sender, EventArgs e)
        {
            if (boardTitle.Equals(""))
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("No Board name");
                alert.SetMessage("Please enter a board name.");
                alert.SetPositiveButton("OK", (senderAlert, args) => { });
                alert.Show();
            }
            else
            {
                MainBoard mb = new MainBoard(beaconId, username, boardTitle.Text);
                if (isPrivateCheckbox.Checked)
                {
                    if (boardPassword.Text.Equals(""))
                    {
                        var alert = new AlertDialog.Builder(this);
                        alert.SetTitle("No password");
                        alert.SetMessage("Please enter a password to create a private board.");
                        alert.SetPositiveButton("OK", (senderAlert, args) => { });
                        alert.Show();
                        return;
                    }
                    else
                    {
                        mb.Password = boardPassword.Text;
                        mb.isPrivateBoard = true;
                    }
                }
                bool created = await db_accessBoard.createNewBoard(beaconId, mb);
                if (created)
                {
                    var boardActivity = new Intent(this, typeof(BoardActivity));
                    boardActivity.PutExtra("_boardType", "MainBoard");
                    boardActivity.PutExtra("_beaconID", beaconId);
                    boardActivity.PutExtra("_username", username);
                    boardActivity.PutExtra("_boardCreator", username);
                    StartActivity(boardActivity);
                }
                else
                {
                    var alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Board Not Created");
                    alert.SetMessage("The board could not be created");
                    alert.SetPositiveButton("OK", (senderAlert, args) => { });
                    alert.Show();
                }
            }
        }

        private async void CancelButton_Click(object sender, EventArgs e)
        {

        }

        private void IsPrivateCheckbox_Click(object sender, EventArgs e)
        {
            if(isPrivateCheckbox.Checked)
            {
                boardPassword.Visibility = ViewStates.Visible;
                passwordText.Visibility = ViewStates.Visible;
            }
            else
            {
                boardPassword.Visibility = ViewStates.Invisible;
                passwordText.Visibility = ViewStates.Invisible;
            }

        }
    }
}