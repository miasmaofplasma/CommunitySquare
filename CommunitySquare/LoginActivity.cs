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
using Newtonsoft.Json;

namespace CommunitySquare
{
    [Activity(Label = "Login")]
    public class LoginActivity : Activity
    {
        private UserInfo user;
        private LoginServerAccess db_access;
        Button loginButton;
        Button createButton;
        TextView loginStatus;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
            db_access = new LoginServerAccess();
            

            loginButton = FindViewById<Button>(Resource.Id.b_login);
            createButton = FindViewById<Button>(Resource.Id.b_createAccount);
            loginStatus = FindViewById<TextView>(Resource.Id.loginStatus);

            loginStatus.Text = "";
            user = null;
            loginButton.Click += LoginButton_Click;
            createButton.Click += CreateButton_Click;

        }

        private async void CreateButton_Click(object sender, EventArgs e)
        {
            var username = FindViewById<EditText>(Resource.Id.et_username).Text;
            var password = FindViewById<EditText>(Resource.Id.et_password).Text;
            loginButton.Clickable = false; //disable login button in the meantime

            loginStatus.Text = "Wait";
            if (db_access.VerifyCredentials(username) && db_access.VerifyCredentials(password)) //verify the user/password
            {
                try //try to connect to user account
                {
                    bool created = await db_access.CreateAccount(username, password); //check to see if the account exists, if not create it with password combination
                    if (created)
                    {
                        loginStatus.Text = "Success";
                        StartActivity(typeof(HomescreenActivity));
                    }
                    else
                    {
                        var alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Username Exists");
                        alert.SetMessage("Please choose another username.");
                        alert.SetPositiveButton("OK", (senderAlert, args) => { });
                        alert.Show();
                    }
                }
                catch (System.Net.WebException)
                {
                    var alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Login Failure");
                    alert.SetMessage("Internet connection error, please try again later");
                    alert.SetPositiveButton("OK", (senderAlert, args) => { });
                    alert.Show();
                }

            }
            else
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Invalid Username or Password");
                alert.SetMessage("Username/Password must contain 3 or more letter and must start with a letter.");
                alert.SetPositiveButton("OK", (senderAlert, args) => { });
                alert.Show();
            }

            loginButton.Clickable = true;
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            var username = FindViewById<EditText>(Resource.Id.et_username).Text;
            var password = FindViewById<EditText>(Resource.Id.et_password).Text;
            createButton.Clickable = false; //disable create account button till login done


            loginStatus.Text = "wait";

            if (db_access.VerifyCredentials(username) || db_access.VerifyCredentials(password)) //check that the password and username meets specifications
            {

                try //try to login, won't work if internet connection is faulty
                {
                    user = await db_access.AtteptLogin(username, password);
                    if (user != null) //successful login mean user is non null, go to next activity
                    {
                        loginStatus.Text = "Login Success";
                        var activityPass = new Intent(this, typeof(HomescreenActivity));
                        activityPass.PutExtra("_username", username);
                        StartActivity(typeof(HomescreenActivity));
                    }
                    else //unsuccessful username/password combination , alert user
                    {
                        var alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Login Failure");
                        alert.SetMessage("Username or Password Incorrect");
                        alert.SetPositiveButton("OK", (senderAlert, args) => { });
                        alert.Show();

                    }
                }
                catch (System.Net.WebException) // web exceptio when system cannot log on to system, alert user
                {
                    var alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Login Failure");
                    alert.SetMessage("Internet connection error, please try again later");
                    alert.SetPositiveButton("OK", (senderAlert, args) => { });
                    alert.Show();
                }
            }
            else  // exception when username/password do not pass requirements, alert user
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Invalid Username or Password");
                alert.SetMessage("Username/Password must contain 3 or more letter and must start with a letter.");
                alert.SetPositiveButton("OK", (senderAlert, args) => { });
                alert.Show();
            }


            createButton.Clickable = true; //reactivate create account button button
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("Username", FindViewById<EditText>(Resource.Id.et_username).Text);
            outState.PutString("Password", FindViewById<EditText>(Resource.Id.et_password).Text);
            base.OnSaveInstanceState(outState);
        }
        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            savedInstanceState.GetString("Username", FindViewById<EditText>(Resource.Id.et_username).Text);
            savedInstanceState.GetString("Password", FindViewById<EditText>(Resource.Id.et_password).Text);
            base.OnRestoreInstanceState(savedInstanceState);
        }





    }
}