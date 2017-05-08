using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloTabWidget
{
	[Activity (Label = "ArtistsActivity")]
	public class ArtistsActivity : Activity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			TextView textview = new TextView (this);
			textview.Text = "This is the Artists tab";
			SetContentView(textview);
		}
	}
}


