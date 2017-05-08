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

namespace ActivityExperiments
{
	[Activity (Label = "Second Activity")]			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Second);

			TextView tvState = FindViewById<TextView> (Resource.Id.stateTextView);
			tvState.Text = "I'm in OnCreate\n";

			Button btnBack = FindViewById<Button> (Resource.Id.backButton);

			btnBack.Click += delegate {
				Intent demoIntent = new Intent(this, typeof(FirstActivity));
				StartActivity(demoIntent);
			};
			
			Button btnFinish = FindViewById<Button> (Resource.Id.finishButton);

			btnFinish.Click += delegate {
				Finish();
			};
			
			Button btnNext = FindViewById<Button> (Resource.Id.nextButton);

			btnNext.Click += delegate {
				Intent thirdIntent = new Intent(this, typeof(ThirdActivity));
				StartActivity(thirdIntent);
			};

		}

		// Lots of things, like the back button will take you here
		// You always go here after OnCreate or OnRestart
		protected override void OnStart()
		{
			base.OnStart ();
			TextView tvState = FindViewById<TextView> (Resource.Id.stateTextView);
			tvState.Text += "I'm in OnStart\n";
		}

		// Returning from an alarm clock screen will take you directly here
		// You always go here after OnStart
		protected override void OnResume()	
		{
			base.OnResume ();
			TextView tvState = FindViewById<TextView> (Resource.Id.stateTextView);
			tvState.Text += "I'm in OnResume\n";
		}

		// Back button from another Activity will bring you here
		protected override void OnRestart()	
		{
			base.OnRestart ();
			TextView tvState = FindViewById<TextView> (Resource.Id.stateTextView);
			tvState.Text += "I'm in OnRestart\n";
		}

		// Lots of things bring you here, then to OnStop, like starting another Activity
		// Another activity coming into focus (an alarm) will take you just to here
		protected override void OnPause()
		{
			base.OnPause ();
			TextView tvState = FindViewById<TextView> (Resource.Id.stateTextView);
			tvState.Text += "I'm in OnPause\n";
		}

		// Starting another Activity will take you here after OnPause
		// Calling Finish will take you to OnPause and then to here
		protected override void OnStop()
		{
			base.OnStop ();
			TextView tvState = FindViewById<TextView> (Resource.Id.stateTextView);
			tvState.Text += "I'm in OnStop\n";
		}


		// Calling Finish will take you to OnPause, OnStop, and then here
		protected override void OnDestroy()
		{
			base.OnDestroy ();
			TextView tvState = FindViewById<TextView> (Resource.Id.stateTextView);
			tvState.Text += "I'm in OnDestroy\n";
		}

}
}

