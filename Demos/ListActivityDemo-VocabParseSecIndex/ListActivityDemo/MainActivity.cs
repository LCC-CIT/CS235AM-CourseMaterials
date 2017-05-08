using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ListActivityDemo
{
	[Activity (Label = "ListActivityDemo", MainLauncher = true)]
	public class MainActivity : ListActivity
	{
		VocabItem[] vocab;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			vocab = new VocabItem[3];
			vocab[0] = new VocabItem("mono", "monkey");
			vocab [1] = new VocabItem ("agua", "water");
			vocab [2] = new VocabItem ("si", "yes");

			ListAdapter = new ArrayAdapter (this, 
			     Android.Resource.Layout.SimpleListItem1,
			     vocab);
		}

		protected override void OnListItemClick(ListView l,
		                                        View v,
		                                        int position,
		                                        long id)
		{
			string word = vocab [position].English;
			Android.Widget.Toast.MakeText(this,
			         word,
			         Android.Widget.ToastLength.Short).Show();
		}

	}


	public class VocabItem
	{
		public string Spanish { get; set; }
		public string English { get; set; }

		public VocabItem(string s, string e)
		{
			Spanish = s;
			English = e;
		}

		public override string ToString ()
		{
			return Spanish;
		}
	}
}


