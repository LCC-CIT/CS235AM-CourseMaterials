
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

using EvolveListView.Model;

namespace EvolveListView
{
	/// <summary>
	/// Demo 5: Add an index and fast-scrolling
	/// </summary>
	[Activity (Label = "Sessions", Icon="@drawable/ic_launcher")]			
	public class SessionsActivity : ListActivity
	{
		private SessionsAdapter adapter;
		private List<Session> sessions;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			sessions = PopulateSessionData ();
			adapter = new SessionsAdapter(this, sessions);
			ListView.Adapter = adapter;
			ListView.FastScrollEnabled = true;
		}

		/// <summary>
		/// Demonstrates how to handle a row click
		/// </summary>
		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			var title = adapter[position].Title;
			//TODO: Demo6: this is how we start the next screen
			var intent = new Intent(this, typeof(SessionActivity));
			intent.PutExtra("Title", title);
			StartActivity(intent);
		}

		/// <summary>
		/// Helper method to populate our session data
		/// </summary>
		protected List<Session> PopulateSessionData()
		{
			return new List<Session> () {
				new Session {Title="Introduction to Mobile Development",Speaker="Bryan Costanich", Location="Ballroom", Begins=new DateTime(2013,4,14,9,0,0)},
				new Session {Title="Hello iOS",Speaker="Bryan Costanich", Location="Ballroom", Begins=new DateTime(2013,4,14,10,0,0)},
				new Session {Title="Hello Android",Speaker="Bryan Costanich", Location="Ballroom", Begins=new DateTime(2013,4,14,15,0,0)},
				new Session {Title="Cross-platform Navigation",Speaker="Bryan Costanich", Location="Ballroom",Begins=new DateTime(2013,4,15,9,0,0)},
				new Session {Title="Cross-platform Projects",Speaker="Bryan Costanich", Location="Ballroom",Begins=new DateTime(2013,4,15,9,0,0)},
				new Session {Title="Keynote (Day 1)",Speaker="Miguel de Icaza, Nat Friedman", Location="Ballroom",Begins=new DateTime(2013,4,16,9,0,0)},
				new Session {Title="Keynote (Day 2)",Speaker="Miguel de Icaza, Nat Friedman", Location="Ballroom",Begins=new DateTime(2013,4,17,9,0,0)},
			};
		}
	}
}

