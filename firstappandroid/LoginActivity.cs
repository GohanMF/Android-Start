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
    [Activity(Label = "LoginActivity" , MainLauncher = true)]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);

            Button ButtonLogin = FindViewById<Button>(Resource.Id.Login);
            EditText LoginName = FindViewById<EditText>(Resource.Id.LoginName);
            EditText LoginPass = FindViewById<EditText>(Resource.Id.LoginPass);



            ButtonLogin.Click += (sender ,e) => 
            {
                var intent = new Intent(this, typeof(MenuActivity));
               StartActivity(intent);
            }; 


        }
    }
}