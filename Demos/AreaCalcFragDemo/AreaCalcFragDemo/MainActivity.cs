using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AreaCalcFragDemo
{
	[Activity (Label = "AreaCalcFragDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// TODO: Only do this for lanscape
			// Load the two fragments that are displayed in the dual-pane layout
			FragmentTransaction ft = FragmentManager.BeginTransaction ();
			var fragment1 = FragmentManager.FindFragmentById (Resource.Id.fragContainer1); 
			if (fragment1 == null)   // We never need to remove this one
				ft.Add (Resource.Id.fragContainer1, new SelectFrag ());
			ft.Commit ();


			// End Todo



		}

		public void DisplayResult(double result)
		{
			TextView resultTextView = FindViewById<TextView> (Resource.Id.resultTextView);
			resultTextView.Text = result.ToString ();
		}
	}
}


