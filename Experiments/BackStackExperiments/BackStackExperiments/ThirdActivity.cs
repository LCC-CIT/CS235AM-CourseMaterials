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
	[Activity (Label = "Third Activity")]			
	public class ThirdActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Third);

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
				Intent thirdIntent = new Intent(this, typeof(SecondActivity));
				StartActivity(thirdIntent);
			};
		}
	}
}

