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

namespace HelloMultiScreenLAB
{
	[Activity (Label = "SecondActivity")]		//FIX------- to recycle this activity instead of creating a new one, also remember OnStart()			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Second);

			var msgPoked = FindViewById<TextView> (Resource.Id.msgPoked);
			msgPoked.Text = Intent.GetStringExtra ("pokeOne") ?? Resources.GetString(Resource.String.missing);

			//handles NUMBER OF POKES message
			int pokeTimes = Intent.GetIntExtra ("pokeTimes", 0);
			var timesPoked = FindViewById<TextView> (Resource.Id.timesPoked);
			timesPoked.SetText(Resource.String.pokedTimesMsg);
			timesPoked.Text += " " + pokeTimes.ToString ();

			Button buttonPoke = FindViewById<Button> (Resource.Id.buttonPokeTwo);

			buttonPoke.Click += delegate {

				var intentFirst = new Intent (this, typeof(MainActivity));
				//intentFirst.PutExtra ("pokeTwo", POKE);
				intentFirst.PutExtra ("pokeTwo", Resources.GetString(Resource.String.pokeTwo));
				intentFirst.PutExtra("pokeTimes", ++pokeTimes);
				StartActivity (intentFirst);
			};

			/*
			 * This segment of code was originally at Lines 38-42.
			 * Second line in is the issue
				var intentFirst = new Intent (this, typeof(MainActivity));
				string msgString = Resource.String.pokeTwo.ToString();
				intentFirst.PutExtra ("pokeTwo", msgString);
				intentFirst.PutExtra("pokeTimes", ++pokeTimes);
				StartActivity (intentFirst);
			*/
		}
	}
}

