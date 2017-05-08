using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PhonewordAndroid
{
	[Activity (Label = "@string/app_name", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private string _translatedNumber;
		private List<string> _phoneNumbers = new List<string>();

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
	
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get a reference to the EditText
			var phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);

			var callLogButton = FindViewById<Button>(Resource.Id.CallLogButton);

			// Set up the Call Button
			var callButton = FindViewById<Button>(Resource.Id.Call);
			callButton.Click += delegate
			{
				// We will place the call using an Intent.
				var callIntent = new Intent(Intent.ActionCall);
				callIntent.SetData(Android.Net.Uri.Parse("tel:" + _translatedNumber));

				// We use an AlertDialog to ensure that the user really wants to place the call.
				var dialogBuilder = new AlertDialog.Builder(this);
				dialogBuilder.SetMessage("Call " + phoneNumberText.Text + "?");
				dialogBuilder.SetNeutralButton("Call", delegate
					{
						_phoneNumbers.Add(_translatedNumber);
						callLogButton.Enabled = true;
						StartActivity(callIntent);
					});
				dialogBuilder.SetNegativeButton("Cancel", delegate {});					
				dialogBuilder.Show();
			};

			// Get our button from the layout resource, and attach an event to it
			var translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
			translateButton.Click += delegate
			{
				_translatedNumber = PhonewordAndroid.Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
				if (String.IsNullOrWhiteSpace(_translatedNumber))
				{
					callButton.Text = Resources.GetString(Resource.String.Call);
					callButton.Enabled = false;
				}
				else
				{
					callButton.Text = Resources.GetString(Resource.String.Call) + " " + _translatedNumber;
					callButton.Enabled = true;
				}
			};

			// Setup the call log button so that it will start up the
			// call log activity, and send it the list of phone numbers that
			// have been called.
			callLogButton.Click += (object sender, EventArgs e) => {
				var intent = new Intent(this, typeof(CallLogActivity));
				intent.PutStringArrayListExtra("phone_numbers", _phoneNumbers);
				StartActivity(intent);
			};


		}
	}
}


