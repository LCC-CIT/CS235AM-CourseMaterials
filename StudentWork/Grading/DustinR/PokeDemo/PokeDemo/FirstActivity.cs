using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MultiscreenDemo
{
	[Activity (Label = "Poke Demo", MainLauncher = true)]
	public class FirstActivity : Activity
	{


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			TextView label = FindViewById<TextView> (Resource.Id.screen1Label);
			Button PokeScreen2 = FindViewById<Button> (Resource.Id.PokeScreen2);

			label.Text = Intent.GetStringExtra("Poke") ?? "";

			PokeScreen2.Click += delegate {
				var poke = new Intent(this, typeof(SecondActivity));
				poke.PutExtra("Poke", "Screen 1 poked you");
				StartActivity(poke);
			};
		}
	}
}


