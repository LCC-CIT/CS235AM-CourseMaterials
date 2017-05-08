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
		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var firstlabel = FindViewById<TextView> (Resource.Id.screen1Label);
			var pokeScreen2 = FindViewById<Button>(Resource.Id.pokeScreen2);

			firstlabel.Text = Intent.GetStringExtra("MessageFromAct2")??"not available";


			
			pokeScreen2.Click += (sender, e) => {
				var poke2 = new Intent(this, typeof(SecondActivity));
				poke2.PutExtra("MessageFromAct1","Screen 1 poked you");
				StartActivity (poke2);
			};
		
		}
	}
}


