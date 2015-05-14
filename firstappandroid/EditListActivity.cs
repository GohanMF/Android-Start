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

using SQLite;
using firstappandroid.Class;
using firstappandroid.Class.DB;

namespace firstappandroid
{
    [Activity(Label = "EditListActivity")]
    public class EditListActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.NewList);

            List<string> allItems = new List<string>();

            /*get interface objects*/
            Button save = FindViewById<Button>(Resource.Id.save);
            Button add = FindViewById<Button>(Resource.Id.add);
            EditText grocText = FindViewById<EditText>(Resource.Id.grocText);
            EditText itemText = FindViewById<EditText>(Resource.Id.itemText);
            ListView listitems = FindViewById<ListView>(Resource.Id.listItems);


            
            /*  load  objects passed from previous view    */

            int idList = 0; 
            idList = Intent.Extras.GetInt("idList");
            SQLiteConnection db = DBConnection.StartConnection();
 
             
            /*  get values from lista and items from the list */

            if (db != null) {
                db_Listas lista = db.Get<db_Listas>(idList);
                
              var  Items = (from l in db.Table<db_items>()
                            where l.Lista_id == idList
                            select l);




                foreach (var i in Items)
                       allItems.Add(i.Name);

                var mylist = FindViewById<ListView>(Resource.Id.listItems);
                mylist.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, allItems);


                grocText.Text = lista.Name;

            }


                
           

            /* give functionality to buttons */
            /*

            add.Click += (object sender, EventArgs e) =>
            {

                if (!string.IsNullOrWhiteSpace(itemText.Text))
                {

                    Console.WriteLine(listitems.Count);
                    allItems.Add(itemText.Text);
                    listitems.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, allItems);

                    itemText.Text = "";
                    Console.WriteLine(listitems.Count);
                }


            };

            save.Click += (object sender, EventArgs e) =>
            {
                if (!string.IsNullOrWhiteSpace(grocText.Text))
                {

                    Console.WriteLine(List_grocerys.Count);

                    Console.WriteLine(grocText.Text);
                    var newitemlist = new db_Listas();
                    newitemlist.Name = grocText.Text;
                    var id = db.Insert(newitemlist);


                    if (allItems.Count() != 0)
                    {

                        foreach (var i in allItems)
                        {
                            var newitem = new db_items();
                            newitem.Name = itemText.Text;
                            newitem.Lista_id = id;
                            db.Insert(newitem);
                        }
                    }


                    Console.WriteLine(List_grocerys.Count);

                }
            };

            */


            save.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MenuActivity));
                StartActivity(intent);
            }; 

        }
    }
}