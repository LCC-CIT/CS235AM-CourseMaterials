﻿using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace PhonewordAndroid
{
    [Activity(Label = "Phoneword Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our UI controls from the loaded layout
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);

            // Disable the "Call" button
            callButton.Enabled = false;

            // Add code to translate number
            string translatedNumber = string.Empty;

            translateButton.Click += delegate
            {
                translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (String.IsNullOrWhiteSpace(translatedNumber)) {
                    callButton.Text = "Call";
                    callButton.Enabled = false;
                }
                else {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;
                }
            };

            // Add callButton event handler here
            callButton.Click += (sender, e) =>
            {
                // On "Call" button click, try to dial phone number.
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("Call " + translatedNumber + "?");
                callDialog.SetNeutralButton("Call", 
                    delegate
                    {
                        // Create intent to dial phone
                        var callIntent = new Intent(Intent.ActionCall);
                        callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                        StartActivity(callIntent);
                    });
                callDialog.SetNegativeButton("Cancel", delegate
                {
                });

                // Show the alert dialog to the user and wait for response.
                callDialog.Show();
            };
        }
    }
}