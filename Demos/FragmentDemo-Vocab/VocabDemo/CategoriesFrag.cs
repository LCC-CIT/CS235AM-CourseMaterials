using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace VocabPractice
{
	public class CategoriesFragment : ListFragment
	{
		List<VocabItem> vocabItemsList;	// All the vocab info from the file (one item per line in the file, one word per line)
		List<string> categories;	// Just the categories. (Each word is in a cateogry like animal, food, etc.)
		bool isDualPane = false;

		public override void OnActivityCreated(Bundle savedInstanceState)
		{ 
			base.OnActivityCreated(savedInstanceState);

			ListAdapter = InitListAdapter ();
			ListView.ChoiceMode = ChoiceMode.Single;

			View detailsFrame = Activity.FindViewById<View>(Resource.Id.words);
			isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;
			if (isDualPane)
			{
				ShowWords(new string[0] , new string[0]);	
			}
		}


		public override void OnListItemClick(ListView l, View v, int position, long id)
		{
			ListView.SetItemChecked(position, true);

			var vocabItems = vocabItemsList.FindAll(x => x.Category == categories[position]);
			string[] spanishWords = new string[vocabItems.Count];
			string[] englishWords = new string[vocabItems.Count];
			for (int i = 0; i < vocabItems.Count; i++) 
			{
				spanishWords[i] = vocabItems[i].Spanish + " (" + vocabItems[i].PartOfSpeech + ")";
				englishWords[i] = vocabItems[i].English;
			}

			ShowWords(englishWords, spanishWords);
		}


		// Parse the vocab text file use its data to make an ArrayAdapter 
		// for the built-in ListView
		private IListAdapter InitListAdapter()
		{
			vocabItemsList = new List<VocabItem>();		// All the vocab info from the file (one item per line in the file, one word per line)

			// The text file will have 4 fields: Category, English word, Spanish word, part of speech, per line
			const int NUMBER_OF_FIELDS = 4;	 
			TextParser parser = new TextParser (", ", NUMBER_OF_FIELDS);	// We will use this to get all the vocab info from the file
			var vocabList = parser.ParseText (Activity.Assets.Open(@"Vocabulary.csd"));		// Open the file as a stream and parse all the text
			vocabList.Sort((x, y) => String.Compare(x[0], y[0],					// sort on category so the section indexer will work.
				StringComparison.Ordinal));

			categories = new List<string>();	// Just the categories. (Each word is in a cateogry like animal, food, etc.)
			// Copy the List of strings into our List of VocabItem objects
			// and build a list of categories (each category should only be put in the list once)
			foreach (string[] wordInfo in vocabList) 
			{
				vocabItemsList.Add (new VocabItem (wordInfo [0], wordInfo [1], wordInfo [2], wordInfo [3]));
				if(!categories.Contains(wordInfo[0]))
					categories.Add(wordInfo [0]);	// This category isn't in the list yet so add it
			}

			return new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItemChecked, categories); 	
		}

		private void ShowWords(string[] englishWords, string[] spanishWords)
		{
			if (isDualPane) 
			{
				// Make new fragment to show this selection. 
				var words = new WordsFragment() { Arguments = new Bundle () };
				words.Arguments.PutStringArray("english", englishWords);
				words.Arguments.PutStringArray("spanish", spanishWords);

				// Execute a transaction, replacing any existing
				// fragment with this one inside the frame.
				var ft = FragmentManager.BeginTransaction(); 
				ft.Replace(Resource.Id.words, words);
				ft.SetTransition(FragmentTransit.FragmentFade);		
				ft.Commit();
			}
			else
			{
				// Otherwise we need to launch a new Activity to display
				// the dialog fragment with selected text.
				var intent = new Intent();
				intent.SetClass(Activity, typeof (WordsActivity));	
				intent.PutExtra ("spanish", spanishWords);	
				intent.PutExtra ("english", englishWords);
				StartActivity(intent);
			}
		}

	}
}

