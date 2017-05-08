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

namespace VocabPractice
{
	[Activity(Label = "Words")]			
	public class WordsActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var wordsFrag = new WordsFragment () { Arguments = Intent.Extras };

			var fragmentTransaction = FragmentManager.BeginTransaction();          
			fragmentTransaction.Add(Android.Resource.Id.Content, wordsFrag);
			fragmentTransaction.Commit();
		}
	}
}

