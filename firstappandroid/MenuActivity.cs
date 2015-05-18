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
using System.IO;
using firstappandroid.Class;
using firstappandroid.Class.DB;

namespace firstappandroid
{
    [Activity(Label = "MenuActivity")]
    public class MenuActivity : Activity
    {
        static List<string> List_grocerys;

        static SQLiteConnection db = DBConnection.StartConnection();
        static SQLite.TableQuery<db_Listas> lista;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            List_grocerys = new List<string>();




            // List_grocerys = new List<string> { "groc 1", "groc 2", "groc 3" };

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Menu);

            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("Minhas Listas", Resource.Drawable.Icon, new SampleTabFragment());
            AddTab("+ New", Resource.Drawable.Icon, new SampleTabFragment2());

            if (bundle != null)
                this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("tab", this.ActionBar.SelectedNavigationIndex);

            base.OnSaveInstanceState(outState);
        }

        void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(Resource.Drawable.Icon);

            // must set event handler before adding tab
            tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };
            tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }



        class SampleTabFragment : Fragment
        {
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                base.OnCreateView(inflater, container, savedInstanceState);

                var view = inflater.Inflate(Resource.Layout.MyList, container, false);
                lista = db.Table<db_Listas>();


                List_grocerys = new List<string>();

                foreach (var i in lista)
                    List_grocerys.Add(i.Name);

                var mylist = view.FindViewById<ListView>(Resource.Id.listView1);
                mylist.Adapter = new ArrayAdapter<string>(view.Context, Android.Resource.Layout.SimpleListItem1, List_grocerys);

                // this.ListAdapter = new ArrayAdapter<string>( Resource.Layout.List, List_grocerys);


                mylist.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) =>
                {

                    Android.Widget.TextView item = (Android.Widget.TextView)e.View;

                    var something = (from l in lista
                                     where l.Name == item.Text
                                     select l).First();

                    // Android.Widget.Toast.MakeText(view.Context, something.Id.ToString(), Android.Widget.ToastLength.Short).Show();
                    var intent = new Intent(view.Context, typeof(EditListActivity));
                    intent.PutExtra("idList", something.Id);
                    StartActivity(intent);
                };



                return view;

            }

        }

        class SampleTabFragment2 : Fragment
        {
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                base.OnCreateView(inflater, container, savedInstanceState);

                var view = inflater.Inflate(Resource.Layout.NewList, container, false);

                List<string> allItems = new List<string>();

                Button save = view.FindViewById<Button>(Resource.Id.save);
                Button add = view.FindViewById<Button>(Resource.Id.add);
                EditText grocText = view.FindViewById<EditText>(Resource.Id.grocText);
                EditText itemText = view.FindViewById<EditText>(Resource.Id.itemText);
                ListView listitems = view.FindViewById<ListView>(Resource.Id.listItems);



                add.Click += (object sender, EventArgs e) =>
                {

                    if (!string.IsNullOrWhiteSpace(itemText.Text))
                    {

                        Console.WriteLine(listitems.Count);
                        allItems.Add(itemText.Text);
                        listitems.Adapter = new ArrayAdapter<string>(view.Context, Android.Resource.Layout.SimpleListItem1, allItems);

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

                        if (db.Insert(newitemlist) > 0)
                        {
                            if (allItems.Count() != 0)
                            {

                                foreach (var i in allItems)
                                {
                                    var newitem = new db_items();
                                    newitem.Name = i;
                                    newitem.Lista_id = newitemlist.Id; 
                                    newitem.bought = false; 
                                    db.Insert(newitem);
                                }
                            }
                        }

                        Console.WriteLine(List_grocerys.Count);

                    }
                };


                return view;
            }
        }
    }
}