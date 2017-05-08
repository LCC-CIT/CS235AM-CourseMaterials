
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

namespace PokeAScreen
{
	[Activity (Label = "SecondActivity")]			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Second);
			var showFirst = FindViewById<Button> (Resource.Id.showFirst);
			
			showFirst.Click += (sender, e) => {
				
				var first = new Intent(this, typeof(FirstActivity));
				first.PutExtra("FirstData", GetString(Resource.String.Scrn2Message));
				StartActivity(first);
			};
			var label = FindViewById<TextView> (Resource.Id.screen2Label);
			label.Text = Intent.GetStringExtra("FirstData");

		}
	}
}