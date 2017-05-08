
using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MultiScreen
{
	[Activity (Label = "First Screen", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		int count = 0;
		bool pop = false;
		Button poker = null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			// Button to 
			poker = FindViewById<Button> (Resource.Id.poker);
			poker.Text = poker.Text + " Second Activity";
			poker.Click += (sender, e) => {
				count++;
				pop = true;
				poker.Enabled = false;

			};

			var showSecond = FindViewById<Button> (Resource.Id.switcher);
			showSecond.Click += (sender, e) => 
			{

				var second = new Intent(this, typeof(SecondActivity));
				second.PutExtra("FirstData", "Data Recieved from 1");
				second.PutExtra("Progress", count);
				second.PutExtra("Pop", pop);
				//second.SetFlags(ActivityFlags.ClearTop);
				StartActivity(second);
				//this.Finish();
				//return;
			};
		}

		protected override void OnResume()
		{
			base.OnResume();

			poker.Enabled = true;

			// Get info from activity 2
			count = Intent.GetIntExtra ("Progress", 0);
			pop = Intent.GetBooleanExtra ("Pop", false);

			var label = FindViewById<TextView> (Resource.Id.label);
			var progress = FindViewById<TextView> (Resource.Id.progress);

			// Display info from activity 2 if the top button was clicked
			if (pop) 	
			{
				label.Text = Intent.GetStringExtra ("SecondData") ?? "Data not available";
				progress.Text = "Count: " + count.ToString ();
			} 
			else 
			{
				label.Text = "No poke recieved";
				progress.Text = "No count received";
			}

		}
	}
}


