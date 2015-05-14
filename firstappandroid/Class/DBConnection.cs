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

using firstappandroid.Class.DB;

namespace firstappandroid.Class
{
    class DBConnection
    {

        public static SQLiteConnection StartConnection(){
        
        
            string dbpath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "listasdemo.db3"
            );

            SQLiteConnection db = new SQLiteConnection(dbpath);


            /*criaçao das tabelas necessarias*/

    


            db.CreateTable<db_Listas>();
            db.CreateTable<db_items>();


            return db;

        }

        public static bool dropdatabase() {

            string dbpath = Path.Combine(
               System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "listasdemo.db3"
               );

            var db = new SQLiteConnection(dbpath);

            db.DeleteAll<db_Listas>();
            db.DeleteAll<db_items>();

            db.Commit();

            return true;

        }

    }
}