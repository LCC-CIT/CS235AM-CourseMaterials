
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using EvolveListView.Model;

namespace EvolveListView
{
	/// <summary>
	/// Demo 2: Custom adapter
	/// </summary>
	public class SpeakersAdapter: BaseAdapter<Speaker>
	{
		private List<Speaker> data;
		private Activity context;

		public SpeakersAdapter(Activity activity, List<Speaker> speakers) 
		{
			data = (from s in speakers orderby s.Name select s).ToList ();
			context = activity;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Speaker this[int position]
		{
			get { return data[position]; }
		}
		public override int Count
		{
			get { return data.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null) {
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
			}

			var speaker = data[position];

			var text = view.FindViewById<TextView>(Android.Resource.Id.Text1);
			text.Text = speaker.Name;

			return view;
		}


		/// <summary>
		/// Helper method to populate our speaker data, 
		/// </summary>
		List<Speaker> GetSpeakerData()
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