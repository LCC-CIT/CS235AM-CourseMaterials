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

namespace MultiScreen
{
	[Activity (Label = "Second Screen", 
	           LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]			
	public class SecondActivity : Activity
	{
		bool pop = false;
		int count = 0;
		Button poker = null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		
			// Create your application here
			SetContentView (Resource.Layout.Second);

			poker = FindViewById<Button> (Resource.Id.poke);
			poker.Text = poker.Text + "First Activity";
			poker.Click += (sender, e) => {
				count++;
				pop = true;
				poker.Enabled = false;
			};

			var showFirst = FindViewById<Button> (Resource.Id.switcher2);
			showFirst.Click += (sender, e) => 
			{
				var first = new Intent(this, typeof(FirstActivity));
				first.PutExtra("SecondData", "Data Recieved from 2");
				first.PutExtra("Progress", count);
				first.PutExtra("Pop", pop);
				//first.SetFlags(ActivityFlags.ClearTop);
				first.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
				var label = FindViewById<TextView> (Resource.Id.screen2Label);
			pop = Intent.GetBooleanExtra ("Pop", false);

			if (pop) {
				label.Text = Intent.GetStringExtra ("FirstData") ?? "Data not available";
			} else {
				Toast.MakeText (this, "Forgot to poke", ToastLength.Short).Show();
				label.Text = "no poke recieved";
			}

			var prog = FindViewById<TextView> (Resource.Id.progressText);
			if (pop) {
				prog.Text = count.ToString () + prog.Text;
			} else {
				prog.Text = "Poke to count";
			}

				StartActivity(first);
				//this.Finish();
				//return;
			};
		}
		
		protected override void OnResume()
		{
			base.OnResume ();

			poker.Enabled = true;
			count = Intent.GetIntExtra ("Progress", 0);
			var label = FindViewById<TextView> (Resource.Id.screen2Label);
			pop = Intent.GetBooleanExtra ("Pop", false);

			if (pop) {
				label.Text = Intent.GetStringExtra ("FirstData") ?? "Data not available";
			} else {
				Toast.MakeText (this, "Forgot to poke", ToastLength.Short).Show();
				label.Text = "no poke recieved";
			}

			var prog = FindViewById<TextView> (Resource.Id.progressText);
			if (pop) {
				prog.Text = "Count: " + count.ToString ();
			} else {
				prog.Text = "Poke to count";
			}

		}
	}
}

