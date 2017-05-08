using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PokeAScreen
{
	[Activity (Label = "Poke-A-Screen", MainLauncher = true)]
	public class FirstActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			
			// Get our button from the layout resource,
			// and attach an event to it
			var label = FindViewById<TextView> (Resource.Id.screen1Label);
			label.Text = Intent.GetStringExtra("FirstData");

			var showSecond = FindViewById<Button> (Resource.Id.showSecond);
			
			showSecond.Click += (sender, e) => {
				
				var second = new Intent(this, typeof(SecondActivity));
				second.PutExtra("FirstData", GetString(Resource.String.Scrn1Message));
				StartActivity(second);
			};
		}
	}
}


