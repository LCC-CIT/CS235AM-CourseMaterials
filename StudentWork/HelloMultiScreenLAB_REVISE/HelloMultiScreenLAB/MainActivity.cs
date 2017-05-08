using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloMultiScreenLAB
{
	[Activity (Label = "HelloMultiScreenLAB", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var msgPoked = FindViewById<TextView> (Resource.Id.msgPoked);
			msgPoked.Text = Intent.GetStringExtra ("pokeTwo") ?? Resources.GetString (Resource.String.pokeTwo);

			//handles NUMBER OF POKES message
			int pokeTimes = Intent.GetIntExtra("pokeTimes", 0);		//if there is no pokeTimes yet, give us a zero there
			var timesPoked = FindViewById<TextView> (Resource.Id.timesPoked);
			timesPoked.SetText(Resource.String.pokedTimesMsg);
			timesPoked.Text += " " + pokeTimes.ToString ();

			Button buttonPoke = FindViewById<Button> (Resource.Id.buttonPoke);
			
			buttonPoke.Click += delegate {
				var intentSecond = new Intent(this, typeof(SecondActivity));
				intentSecond.PutExtra("pokeOne", Resources.GetString(Resource.String.pokeOne));
				intentSecond.PutExtra("pokeTimes", ++pokeTimes);
				StartActivity(intentSecond);
			};
		}
	}
}


