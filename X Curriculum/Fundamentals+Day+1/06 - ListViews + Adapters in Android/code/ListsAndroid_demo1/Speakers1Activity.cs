
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
	/// <summary>
	/// Demo 1: Populate a ListView with an ArrayAdapter
	/// </summary>
	[Activity (Label = "ListView1", MainLauncher = true, Icon="@drawable/ic_launcher")]			
	public class SessionsActivity : ListActivity
	{
		string[] items;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			//TODO: Demo1: uncomment this to display data in the list 
//			items = new string[] { "Miguel de Icaza", "Nat Friedman", "Bart Decrem", "Scott Hanselman" };
//			ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
		}
	
		/// <summary>
		/// Demonstrates how to handle a row click
		/// </summary>
		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			//TODO: Demo1: uncomment this to display a Toast notification on row click
//			var t = items[position];
//			Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();

			//TODO: Demo1: uncomment this to open a new screen
//			var intent = new Intent(this, typeof(SpeakerActivity));
//			intent.PutExtra("Name", t);
//			StartActivity(intent);
		}
	}
}