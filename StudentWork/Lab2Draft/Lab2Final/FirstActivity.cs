using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloMultiScreen
{
	[Activity (Label = "FirstActivity", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		// Not a remnant. Usings this to count how many times darwin was poked
		public  int count = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Use UI created in Main.xml
			SetContentView (Resource.Layout.Main);

			// Get reference to TextView screen1Label
			var label = FindViewById<TextView> (Resource.Id.screen1Label);

			// Display poke from screen 2 only if this is not the initial application launch
			if (count > 0) {
				var str = label.Text;
				label.Text = Intent.GetStringExtra ("SecondData") ?? GetString(Resource.String.noData);
				if (count%4 != 0)label.Text += "\n" + str;
				else label.Text += "\n" + GetString(Resource.String.nm) + str;
			}
			label.Text += " count: " + count.ToString();

			// Get reference to button
			var showSecond = FindViewById<Button> (Resource.Id.showSecond);
			
			// Set click handler anonynos function
			showSecond.Click += (Sender, e) => {
				var second = new Intent(this, typeof(SecondActivity));
				second.PutExtra ("FirstData", GetString(Resource.String.poke1));
				StartActivity (second);
			};
		}

		protected override void OnResume()
		{
			base.OnResume ();
			count++;
		}
	}
}


