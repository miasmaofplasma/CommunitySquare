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
    [Activity(Label = "BoardActivity")]
    public class BoardActivity : ListActivity
    {
        string[] testData = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "10", "11", "12" };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BoardActivity);

            ArrayAdapter adapter = new ArrayAdapter(this,
                Resource.Layout.MessageView, testData);
            ListAdapter = adapter;

            // Create your application here
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            Toast.MakeText(this, testData[position], 
                ToastLength.Short).Show();
        }
    }
}