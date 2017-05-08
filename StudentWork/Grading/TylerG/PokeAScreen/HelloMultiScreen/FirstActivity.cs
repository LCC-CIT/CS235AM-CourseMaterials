using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloMultiScreen
{
	[Activity (Label = "HelloMultiScreen", MainLauncher = true)]
	public class FirstActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var showSecond = FindViewById<Button> (Resource.Id.showSecond);
			showSecond.Click += delegate { 
			var second = new Intent(this, typeof(SecondActivity));
			second.PutExtra("FirstData", "Data from FirstActivity");
			StartActivity (second);
			};
		}
	}
}


