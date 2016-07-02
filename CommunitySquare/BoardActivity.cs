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
    public class BoardActivity : ListActivity
    {
        BoardServerAccess db_accessBoard;
        MessageServerAccess db_accessMessage;

        IBoard messageBoard;


        string[] messageData;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BoardActivity);
            
            db_accessBoard = new BoardServerAccess();
            db_accessMessage = new MessageServerAccess();

            ArrayAdapter adapter = new ArrayAdapter(this,
                Resource.Layout.MessageView, messageData);
            ListAdapter = adapter;


            // Create your application here
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            Toast.MakeText(this, messageData[position], 
                ToastLength.Short).Show();
        }
    }
}