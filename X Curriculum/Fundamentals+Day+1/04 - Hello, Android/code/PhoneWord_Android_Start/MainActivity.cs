using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PhonewordAndroid
{
	[Activity (Label = "Phoneword", MainLauncher = true)]
	public class Activity1 : Activity
	{
		private string _translatedNumber;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
	
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
	
			// Get our button from the layout resource,
			// and attach an event to it
//			Button TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
//			Button CallButton = FindViewById<Button>(Resource.Id.Call);
//			EditText PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
	
//			TranslateButton.Click += delegate
//			{
//		
//				_translatedNumber = Core.PhonewordTranslator.ToNumber(PhoneNumberText.Text);
//		
//				if (_translatedNumber == "")
//				{
//					CallButton.Text = Resources.GetString(Resource.String.Call);
//					CallButton.Enabled = false;
//				}
//				else
//				{
//					CallButton.Text = Resources.GetString(Resource.String.Call) + " " + _translatedNumber;
//					CallButton.Enabled = true;
//				}
//			};
//	
//			CallButton.Click += delegate
//			{
//				// We will place the call using an Intent.
//				var callIntent = new Intent(Intent.ActionCall);
//				callIntent.SetData(Android.Net.Uri.Parse("tel:" + _translatedNumber));
//
//				var callDialog = new AlertDialog.Builder(this)
//					.SetMessage("Call " + PhoneNumberText.Text + "?")
//		    		.SetNeutralButton("Call", delegate
//						{
//							StartActivity(callIntent);
//						})
//					.SetNegativeButton("Cancel", delegate {});
//				callDialog.Show();
//			};
		}
	}
}


