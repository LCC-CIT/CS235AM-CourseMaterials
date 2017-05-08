using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PokeAScreen
{
	[Activity (Label = "Poke A Screen (Screen 2)")]
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.SecondScreen);
			
			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				Intent intent = new Intent(this, typeof(FirstActivity));
				intent.PutExtra(FirstActivity.POKE, GetString(Resource.String.pokedByScreen2));
				StartActivity(intent);
			};

			TextView textView = FindViewById<TextView> (Resource.Id.textView);
			textView.Text = Intent.GetStringExtra(FirstActivity.POKE) ?? "";
		}
	}
}