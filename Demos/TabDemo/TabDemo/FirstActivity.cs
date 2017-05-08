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

namespace TabDemo
{
	[Activity (Label = "FirstActivity")]			
	public class FirstActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.First);

			Button btnSendToTwo = FindViewById<Button> (Resource.Id.btnSendToTwo);

			btnSendToTwo.Click += delegate 
			{
				var host = (TabHostActivity)Parent;
				host.Info = "Hello from the first activity!";
				host.TabHost.CurrentTab = 1;
			};
		}

		protected override void OnResume ()
		{
			TextView tv = FindViewById<TextView> (Resource.Id.txtCount);
			tv.Text = ((TabHostActivity)Parent).Count.ToString();
			base.OnResume ();
		}
	}
}

