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

namespace firstappandroid
{
    [Activity(Label = "List")]
    public class MyListActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            var items = Intent.Extras.GetStringArrayList("items_List") ?? new string[0];
            this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
              

            // Create your application here
        }
    }
}