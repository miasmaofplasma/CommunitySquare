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
    [Activity(Label = "UserOptionsActivity")]
    public class UserOptionsActivity : Activity
    {
        Button deleteAccountButton;
        Button logoutButton;
        LoginServerAccess db_accessLogin;
        string username;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserOptions);
            db_accessLogin = new LoginServerAccess();

            username = Intent.GetStringExtra("_username");

            deleteAccountButton = FindViewById<Button>(Resource.Id.b_deleteUser);
            deleteAccountButton.Click += DeleteAccountButton_Click;

            logoutButton = FindViewById<Button>(Resource.Id.b_logout);
            logoutButton.Click += LogoutButton_Click;
        }

        private async void LogoutButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            EditText input = new EditText(this);
            var alert = new AlertDialog.Builder(this);
            input.SetRawInputType(InputTypes.TextVariationPassword);
            alert.SetView(input);
            alert.SetTitle("Delete Account?");
            alert.SetMessage("You are about to delete your account deleting all messages and boards you have created.  Enter password to continue.");
            alert.SetPositiveButton("OK", async (senderAlert, args) =>
            {
                UserInfo user = await db_accessLogin.AtteptLogin(username, input.Text);
                if(user == null)
                {
                    var alert2 = new AlertDialog.Builder(this);
                    alert2.SetTitle("Incorrect Password");
                    alert2.SetMessage("Password Incorrect");
                    alert2.SetPositiveButton("OK", (senderAlert2, args2) => { });
                    alert2.Show();
                    StartActivity(typeof(LoginActivity));
                }
                else
                {
                    db_accessLogin.DeleteAcount(user);
                }

            });
            alert.SetNegativeButton("Cancel", (senderAlert, args) => { });
            alert.Show();

        } 
    }
}