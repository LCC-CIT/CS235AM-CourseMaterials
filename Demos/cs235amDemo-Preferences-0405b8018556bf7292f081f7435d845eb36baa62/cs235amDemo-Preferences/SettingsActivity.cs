
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
using Android.Preferences;

namespace cs235amDemoPreferences
{
	[Activity (Label = "SettingsActivity")]			
	public class SettingsActivity : PreferenceActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			string[] greetings = new string[] {
				"Hello", "Hi", "Hola", "Yo", "Howdy Ya'll", "Merhaba",
				"Salam"};

			base.OnCreate (bundle);

			AddPreferencesFromResource (Resource.Xml.Preferences);

		}
	}
}

