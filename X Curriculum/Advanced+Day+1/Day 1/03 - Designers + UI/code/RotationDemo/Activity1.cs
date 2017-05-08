using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RotationDemo
{
	[Activity (Label = "RotationDemo", MainLauncher = true, Icon="@drawable/Icon")]
    public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);
		}
	}
}