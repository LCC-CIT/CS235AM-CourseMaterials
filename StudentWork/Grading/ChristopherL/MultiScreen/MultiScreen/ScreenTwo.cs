
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

namespace MultiScreen
{
	[Activity (Label = "ScreenTwo")]			
	public class ScreenTwo : Activity
	{
		private const string PokeForScreenOne = "Screen 2 Poked You";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.ScreenTwo);

			//Set TextView stuff
			TextView textViewTwo = FindViewById<TextView> (Resource.Id.ScreenTwoText);
			
			string textFromScreenTwo = Intent.GetStringExtra(ScreenOne.ScreenOneData) ?? ScreenOne.NoData;
			if (!textFromScreenTwo.Equals(ScreenOne.NoData))
				textViewTwo.Text = textFromScreenTwo;
			else 
				textViewTwo.SetText(Resource.String.Screen2DefaultText);

			//Set Button stuff
			Button buttonTwo = FindViewById<Button> (Resource.Id.ScreenTwoButton);

			buttonTwo.Click += (sender, e) => 
			{
				Intent screenOne = new Intent(this, typeof(ScreenOne));
				screenOne.PutExtra(ScreenOne.ScreenTwoData, PokeForScreenOne );
				SetResult(Result.Ok, screenOne);
				Finish();
			};
		}
	}
}

