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
    public class MessageAdapter : BaseAdapter<Message>
    {
        private List<Message> messageList;
        private Activity _context;


        public MessageAdapter(Activity context, List<Message> messageList)
        {
            this._context = context;
            this.messageList = messageList;
        }
        public override Message this[int position]
        {
            get
            {
                return messageList[position];
            }
        }

        public override int Count
        {
            get
            {
                return messageList.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var message = messageList[position];
            View view = convertView;
            if (view == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.MessageView, null);
            }
            view.FindViewById<TextView>(Resource.Id.textItem).Text = message.Title;
            view.FindViewById<TextView>(Resource.Id.textItem2).Text = message.Creator;
            return view;
                 
        }
    }
}