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

using firstappandroid.Class.DB;

namespace firstappandroid.Adapters
{
    class CustomViewAdapter : BaseAdapter<db_items>
    {

        List<db_items> items;
        Activity context;

        public CustomViewAdapter(Activity context , List<db_items> items)
            : base()
        {
            this.context = context;
            this.items = items;

        }

        public override long GetItemId(int position)
        {

            return items[position].Id;

        }

        public override db_items this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var item = items[position];
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.customView, null);

            view.FindViewById<CheckedTextView>(Resource.Id.listitem).Text = item.Name;
            view.FindViewById<CheckedTextView>(Resource.Id.listitem).Checked = item.bought;

            return view;
        }
    }
}