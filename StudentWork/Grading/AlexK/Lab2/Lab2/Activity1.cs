using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Lab2
{
	[Activity (Label = "Lab2", MainLauncher = true)]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var view1 = FindViewById<TextView>(Resource.Id.Activity1Label);
			view1.Text = Intent.GetStringExtra("Poke") ?? "Data not available";

			Button button = FindViewById<Button> (Resource.Id.Activity1Button);
			button.Click += (sender, e) => {
				var intent1 = new Intent(this, typeof(Activity2));
				intent1.PutExtra("Poke", this.Resources.GetString(Resource.String.Button1PokeMessage));
				StartActivity(intent1);
			}; 
		}
	}
}


