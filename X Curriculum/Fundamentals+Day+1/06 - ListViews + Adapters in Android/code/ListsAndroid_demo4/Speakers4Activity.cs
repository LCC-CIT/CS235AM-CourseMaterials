
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
	/// Demo 4: Custom row layout
	/// </summary>
	[Activity (Label = "ListView4", MainLauncher = true, Icon="@drawable/ic_launcher")]			
	public class SpeakersActivity : ListActivity
	{
		private SpeakersAdapter adapter;
		private List<Speaker> speakers;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			speakers = GetSpeakerData ();
			adapter = new SpeakersAdapter(this, speakers);
			ListView.Adapter = adapter;
		}

		/// <summary>
		/// Demonstrates how to handle a row click
		/// </summary>
		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			var speakerName = adapter[position].Name;
			Toast.MakeText(this, "You selected the speaker " + speakerName + ".", ToastLength.Short).Show();
		}

		/// <summary>
		/// Helper method to populate our speaker data, 
		/// </summary>
		protected List<Speaker> GetSpeakerData()
		{
			return new List<Speaker> () {
				new Speaker { Name = "John Zablocki", Company = "Xamarin", HeadshotUrl = "images/speakers/john_zablocki.jpg", TwitterHandle ="codevoyeur" },
				new Speaker { Name = "Miguel de Icaza", Company = "Xamarin", HeadshotUrl = "images/speakers/miguel.jpg", TwitterHandle ="migueldeicaza"},
				new Speaker { Name = "Aaron Bockover", Company = "Xamarin", HeadshotUrl = "images/speakers/aaron.jpg", TwitterHandle ="abock" },
				new Speaker { Name = "John Bubriski", Company = "Xamarin", HeadshotUrl = "images/speakers/john_bubriski.jpg", TwitterHandle ="johnbubriski" },
				new Speaker { Name = "Paul Betts", Company = "Xamarin", HeadshotUrl = "images/speakers/paul.jpg", TwitterHandle ="xpaulbettsx" },
				new Speaker { Name = "Louis DeJardin", Company = "Xamarin", HeadshotUrl = "images/speakers/louis.jpg", TwitterHandle ="loudej" },
				new Speaker { Name = "Scott Olson", Company = "Xamarin", HeadshotUrl = "images/speakers/scott.jpg", TwitterHandle ="vagabondrev" },
				new Speaker { Name = "Igor Moochnick", Company = "Xamarin", HeadshotUrl = "images/speakers/igor.jpg", TwitterHandle ="igor_moochnick" },
				new Speaker { Name = "Jérémie Laval", Company = "Xamarin", HeadshotUrl = "images/speakers/jeremie.jpg", TwitterHandle ="jeremie_laval" },
				new Speaker { Name = "Ananth Balasubramaniam", Company = "Xamarin", HeadshotUrl = "images/speakers/ananth.jpg", TwitterHandle ="ananthonline" },
				new Speaker { Name = "Bob Familiar", Company = "Xamarin", HeadshotUrl = "images/speakers/bob.jpg", TwitterHandle ="bobfamiliar" },
				new Speaker { Name = "Michael Hutchinson", Company = "Xamarin", HeadshotUrl = "images/speakers/michael.jpg", TwitterHandle ="mjhutchinson" },
				new Speaker { Name = "Jonathan Chambers", Company = "Xamarin", HeadshotUrl = "images/speakers/jonathan.jpg", TwitterHandle ="jon_cham" },
				new Speaker { Name = "Steve Millar", Company = "Xamarin", HeadshotUrl = "images/speakers/steve.jpg", TwitterHandle ="samillar77" },
				new Speaker { Name = "Somya Jain", Company = "Xamarin", HeadshotUrl = "images/speakers/somya.jpg", TwitterHandle ="somya_j" },
				new Speaker { Name = "Sam Lippert", Company = "Xamarin", HeadshotUrl = "images/speakers/sam.jpg", TwitterHandle ="lippertz" },
				new Speaker { Name = "Don Syme", Company = "Xamarin", HeadshotUrl = "images/speakers/don.jpg", TwitterHandle ="dsyme" },
				new Speaker { Name = "Dean Ellis", Company = "Xamarin", HeadshotUrl = "images/speakers/dean.jpg", TwitterHandle ="infspacestudios" },
				new Speaker { Name = "Jb Evain", Company = "Xamarin", HeadshotUrl = "images/speakers/jb.jpg", TwitterHandle ="jbevain" },
				new Speaker { Name = "Chris Hardy", Company = "Xamarin", HeadshotUrl = "images/speakers/chris.jpg", TwitterHandle ="chrisntr" },
				new Speaker { Name = "Demis Bellot", Company = "Xamarin", HeadshotUrl = "images/speakers/demis.jpg", TwitterHandle ="demisbellot" },
				new Speaker { Name = "Frank Krueger", Company = "Xamarin", HeadshotUrl = "images/speakers/frank.jpg", TwitterHandle ="praeclarum" },
				new Speaker { Name = "Greg Shackles", Company = "Xamarin", HeadshotUrl = "images/speakers/greg.jpg", TwitterHandle ="gshackles" },
				new Speaker { Name = "Phil Haack", Company = "Xamarin", HeadshotUrl = "images/speakers/phil.jpg", TwitterHandle ="haacked" },
				new Speaker { Name = "David Fowler", Company = "Xamarin", HeadshotUrl = "images/speakers/david.jpg", TwitterHandle ="davidfowler" },
			};  		
		}
	}
}

