using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MultiScreen
{
	[Activity (Label = "MultiScreen", MainLauncher = true)]
	public class ScreenOne : Activity
	{
		//These are the constant strings
		public const string NoData = "No Data Found";
		public const string ScreenTwoData = "Data From Screen Two";
		public const string ScreenOneData = "Data From Screen One";
		private const string PokeForScreenTwo = "Screen 1 Poked You";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.ScreenOne);

			//Set Button stuff
			Button buttonOne = FindViewById<Button> (Resource.Id.ScreenOneButton);

			buttonOne.Click += (sender, e) => 
			{
				Intent screenTwo = new Intent(this, typeof(ScreenTwo));
				screenTwo.PutExtra(ScreenOneData, PokeForScreenTwo );
				StartActivityForResult(screenTwo, 1);
			};
		}

		//This is for when ScreenTwo Finishes
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (requestCode == 1)
			{
				//Set TextView Stuff
				TextView textViewOne = FindViewById<TextView> (Resource.Id.ScreenOneText);
				if (resultCode == Result.Ok)
				{
					string textFromScreenTwo = data.GetStringExtra(ScreenTwoData) ?? NoData;
					if (!textFromScreenTwo.Equals(NoData))
						textViewOne.Text = textFromScreenTwo;
					else 
						textViewOne.SetText(Resource.String.ErrorString);
				}
				else
				{
					textViewOne.SetText(Resource.String.ErrorString);
				}
			}
		}
	}
}


