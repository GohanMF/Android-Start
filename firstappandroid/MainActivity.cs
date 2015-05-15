using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using core;
using System.Collections.Generic;

namespace firstappandroid
{
    [Activity(Label = "Phone Word", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
    static readonly List<string> phoneNumbers = new List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            Button callHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);

       


            callButton.Enabled = false;
            var translatednumber = string.Empty;
            translateButton.Click += (object sender, EventArgs e) =>
            {
                translatednumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (!string.IsNullOrWhiteSpace(translatednumber))
                {
                    callButton.Text = "Call " + translatednumber;
                    callButton.Enabled = true;
                }
                else
                {

                    callButton.Text = "Call";
                    callButton.Enabled = false;
                }

            };


            callButton.Click += (object sender, EventArgs e) =>
            {

                var callDialog = new AlertDialog.Builder(this);

                callDialog.SetMessage("call " + translatednumber + "?");
                callDialog.SetNeutralButton("call", delegate
                {
                    phoneNumbers.Add(translatednumber);
                    callHistoryButton.Enabled = true;

                    var callIntet = new Intent(Intent.ActionCall);
                    callIntet.SetData(Android.Net.Uri.Parse("tel:" + translatednumber));
                    StartActivity(callIntet);
                });
                callDialog.SetNegativeButton("Cancel", delegate { });

                callDialog.Show();

            };

            callHistoryButton.Click += (sender, e) =>
                {
                    var intent = new Intent(this, typeof(CallHistoryActivity));
                    intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                    StartActivity(intent);
                };
           
        }
    }
}

