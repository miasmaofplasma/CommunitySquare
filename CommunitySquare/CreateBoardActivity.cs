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
    [Activity(Label = "CreateBoardActivity")]
    public class CreateBoardActivity : Activity
    {
        BoardServerAccess db_accessBoard;
        Button createBoardButton;
        CheckBox isPrivateCheckbox;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            createBoardButton.Click += CreateBoardButton_Click;

            // Create your application here
        }

        private void CreateBoardButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}