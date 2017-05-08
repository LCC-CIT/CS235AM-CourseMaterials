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

namespace TabDemo
{
	[Activity (Label = "SecondActivity")]			
	public class SecondActivity : Activity
	{
		TabHostActivity parentActivity = null;	// we'll use this in more than one place, so make it a class member


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Second);

			parentActivity = (TabHostActivity)this.Parent;	// Get a reference to the parent activity

			Button btnCount = FindViewById<Button> (Resource.Id.btnCount);

			// Just count the number of times the user clicks the button
			btnCount.Click += delegate 
			{
				parentActivity.Count++;			// Store the count in a property on the parent
				TextView tv = FindViewById<TextView> (Resource.Id.txtCount);
				tv.Text = parentActivity.Count.ToString();
			};

		}
		

		// Update the information displayed by this activity
		// when the user selects this activity's tab
		protected override void OnResume ()
		{
			base.OnResume ();
			var tvMessage = FindViewById<TextView> (Resource.Id.txtMessage);
			tvMessage.Text = parentActivity.Info;
			var tvCount = FindViewById<TextView> (Resource.Id.txtCount);
			tvCount.Text = parentActivity.Count.ToString();
		}
	}
}

