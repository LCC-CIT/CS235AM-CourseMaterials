
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

namespace HelloMultiScreen
{
	[Activity (Label = "SecondActivity")]			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle); 

			// Create your application here
			SetContentView (Resource.Layout.Second);

			var label = FindViewById<TextView> (Resource.Id.screen2Label);

			var str = label.Text;
			label.Text = Intent.GetStringExtra ("FirstData") ?? GetString(Resource.String.noData);
			label.Text += "\n";
			/*
			for (int i = 0; i <= FirstActivity.Count%4; i++) {
				if (i != 3) label.Text += str;
				else label.Text += GetString(Resource.String.stopIt);
			}
			FirstActivity.Count++;
			*/
			var showFirst = FindViewById<Button> (Resource.Id.showFirst);

			showFirst.Click += (Sender, e) => {
				var first = new Intent(this, typeof(FirstActivity));
				first.PutExtra ("SecondData", GetString(Resource.String.poke2));
				StartActivity (first);
			};
		}
	}
}

