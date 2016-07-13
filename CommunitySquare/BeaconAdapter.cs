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
using Java.Lang;

namespace CommunitySquare
{
    public class BeaconAdapter : BaseAdapter<BeaconView>
    {
        List<BeaconView> items;
        Activity context;
        public BeaconAdapter(Activity context, List<BeaconView> items) : base()
        {
            this.items = items;
            this.context = context;
        }

        public override BeaconView this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.MessageView, null);
            }
            view.FindViewById<TextView>(Resource.Id.textItem).Text = item.BoardName;
            view.FindViewById<TextView>(Resource.Id.textItem2).Text = item.BeaconId;
            return view;
        }
    }
}