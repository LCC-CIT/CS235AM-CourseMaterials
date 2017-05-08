using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CustomAdapterDemo;
using System.Collections.Generic;

namespace ListAndParser
{
	[Activity (Label = "ListAndParser", MainLauncher = true)]
	public class MainActivity : ListActivity
	{
		List<VocabItem> vocabItems;		// Since we're using a custom adapter we can use a List instead of an array

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			vocabItems = new List<VocabItem>();		// Lists are more convenient than arrays

			const int NUMBER_OF_FIELDS = 3;	 // The text file will have 3 fields, English word, Spanish word, part of speech, per line
			TextParser parser = new TextParser (", ", NUMBER_OF_FIELDS);	// We will use this to get all the vocab info from the file
			var vocabList = parser.ParseText (Assets.Open(@"Vocabulary.csd"));		// Open the file as a stream and parse all the text
			vocabList.Sort((x, y) => String.Compare(x[2], y[2],					// sort on part of speech so the section indexer will work.
			                                        StringComparison.Ordinal));

			// Copy the List of strings into our List of VocabItem objects
			foreach(string[] wordInfo in vocabList)
				vocabItems.Add(new VocabItem(wordInfo[0], wordInfo[1], wordInfo[2]));

			// Instantiate our custome list adapter
			ListAdapter = new VocabAdapter (this, vocabItems);

			// This is all you need to do to enable fast scrolling
			ListView.FastScrollEnabled = true;
		}

		// Pop up a toast that shows the English translation of the Spanish word
		protected override void OnListItemClick(ListView l,
		                                        View v,
		                                        int position,
		                                        long id)
		{
			string word = vocabItems[position].English;
			Android.Widget.Toast.MakeText(this,
			                              word,
			                              Android.Widget.ToastLength.Short).Show();
		}

	}


	// Cutom list adapter class
	public class VocabAdapter : BaseAdapter<VocabItem>, ISectionIndexer	// Don't forget to inherit from the interface too!
	{
		List<VocabItem> items;	// List of vocabluary words (includes Spanish, English and part of speech)
		Activity context;		// The activity we are running in

		public VocabAdapter(Activity c, List<VocabItem> i ) : base()
		{
			items = i;
			context = c;
			BuildSectionIndex();
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override VocabItem this[int position]
		{
			get { return items [position];}
		}

		public override int Count
		{
			get { return items.Count;}
		}

		public override View GetView (int position, 
		                              View convertView, 
		                              ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate (
					Android.Resource.Layout.TwoLineListItem,
					null);
			view.FindViewById<TextView> (Android.Resource.Id.Text1).Text
				= items [position].Spanish;
			view.FindViewById<TextView> (Android.Resource.Id.Text2).Text
				= items [position].PartOfSpeech;
			return view;
		}

		// -- Code for the ISectionIndexer implementation follows --
		String[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex; 

		public int GetPositionForSection(int section)
		{
			return alphaIndex [sections [section]];
		}

		public int GetSectionForPosition(int position)
		{
			return 1;
		}

		public Java.Lang.Object[] GetSections()
		{
			return sectionsObjects;
		}

		private void BuildSectionIndex()
		{
			alphaIndex = new Dictionary<string, int>();		// Map sequential numbers
			for (var i = 0; i < items.Count; i++)
			{
				// Use the part of speech as a key
				var key = items[i].PartOfSpeech;
				if (!alphaIndex.ContainsKey(key))
				{
					alphaIndex.Add(key, i);
				} 
			}

			sections = new string[alphaIndex.Keys.Count];
			alphaIndex.Keys.CopyTo(sections, 0);
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for (var i = 0; i < sections.Length; i++)
			{
				sectionsObjects[i] = new Java.Lang.String(sections[i]);
			}
		} 

	} 
}



