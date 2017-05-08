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

namespace IntentAndBundleExperiment
{
	[Activity (Label = "DataActivity", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]			
	public class DataActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{

			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Data);
			TextView tvName = FindViewById<TextView> (Resource.Id.nameTextView);
			tvName.Text = Intent.GetStringExtra ("Name") ?? "No name";

			TextView tvBirthday = FindViewById<TextView> (Resource.Id.birthdayTextView);
			tvBirthday.Text = Intent.GetStringExtra ("Birthday") ?? "No birthday";

			TextView tvAge = FindViewById<TextView> (Resource.Id.ageTextView);
			tvAge.Text = Intent.GetStringExtra ("Age") ?? "No age";

		}
	}
}

