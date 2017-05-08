using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace L1C3Resources
{
	[Activity (Label = "L1C3Resources", MainLauncher = true)]
	public class Activity1 : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			ImageView image = FindViewById<ImageView> (Resource.Id.myImageView);
			
			button.Click += delegate {
				if (count % 2 == 0)
					// Even values of count, so display flag_even
					image.SetImageResource(Resource.Drawable.flag_even);
				else
					image.SetImageResource(Resource.Drawable.flag_odd);
				button.Text = GetString (Resource.String.button_clicked_text, count++);
			};
		}
	}
}


