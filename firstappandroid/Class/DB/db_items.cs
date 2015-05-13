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

namespace firstappandroid.Class.DB
{
    public class db_items
    {
        [PrimaryKey, AutoIncrement]
        public int Id {get;set;}
        [MaxLength(50)]
        public string Name {get;set;}

        public int Lista_id { get; set; }

    }
}