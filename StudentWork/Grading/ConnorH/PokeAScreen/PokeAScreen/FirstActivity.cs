using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PokeAScreen
{
	[Activity (Label = "Poke A Screen (Screen 1)", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		public static String POKE { get { return "poke"; } }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				Intent intent = new Intent(this, typeof(SecondActivity));
				intent.PutExtra(POKE, GetString(Resource.String.pokedByScreen1));
				StartActivity(intent);
			};
			
			TextView textView = FindViewById<TextView> (Resource.Id.textView);
			textView.Text = Intent.GetStringExtra(POKE) ?? "";
		}
	}
}