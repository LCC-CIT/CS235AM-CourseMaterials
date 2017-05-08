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

namespace RpsTabDemo
{
	[Activity (Label = "Winner Activity", ParentActivity = typeof(MainActivity))]			
	public class WinnerActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Winner);

			var name = Intent.GetStringExtra("winning_hand_position");
			TextView nameTextView = FindViewById<TextView> (Resource.Id.handTextView);
			nameTextView.Text = name + " is the winner!";

			ActionBar.SetDisplayHomeAsUpEnabled (true);
		}
	}
}

