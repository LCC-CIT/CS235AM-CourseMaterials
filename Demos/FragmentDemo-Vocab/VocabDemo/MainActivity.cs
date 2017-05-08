/* VocabPractice Fragments Demo 						*
 * By Brian Bird										*
 * 11/20/13												*
 * The puprose of the app is to demonstrate the use		*
 * of fragments.										*/

using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

// Only for Honeycomb (Android 3.0, API 11) or later

namespace VocabPractice
{
	[Activity (Label = "Vocabulary Practice", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.categoriesFrag);	// Reference the layout by it's file name
		}

	}
}



