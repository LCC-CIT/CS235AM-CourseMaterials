using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ActivityExperiments
{
	// Standard launch mode: a new instance of this Activity will be created every time it is started
	// This instance will be placed at the top of the current task's stack and multiple copies may exist in a stack
	// This instance will be managed in the normal way by the stack (move down, up, be popped off)
	[Activity (Label = "First Activity", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		int startCount = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button btnStart = FindViewById<Button> (Resource.Id.startButton);
			
			btnStart.Click += delegate {
				Intent mainIntent = new Intent(this, typeof(SecondActivity));
				StartActivity(mainIntent);
			};

			startCount++;
			TextView tvNumStarts = FindViewById<TextView> (Resource.Id.tvNumStarts);
			tvNumStarts.Text = "This instance started " + startCount.ToString () + " times";
		}
	}
}


