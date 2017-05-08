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

namespace EvolveListView
{
	[Activity (Label = "ListView6", MainLauncher = true, Icon="@drawable/ic_launcher")]			
	public class MenuActivity : ListActivity
	{
		string[] items;
		
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			items = new string[] { "Sessions", "Speakers", "About" };
			ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
		}
		
		/// <summary>
		/// Demonstrates how to handle a row click
		/// </summary>
		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			//TODO: Demo6: hardcoded the menu options
			var intent = new Intent(this, typeof(SessionsActivity));

			if (position == 1) 
				intent = new Intent (this, typeof(SpeakersActivity));
			else if (position == 2)
				intent = new Intent (this, typeof(AboutActivity));

			StartActivity(intent);
		}
	}
}