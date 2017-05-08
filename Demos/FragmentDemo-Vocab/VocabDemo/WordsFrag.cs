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
	public class WordsFragment : Fragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup 
			container, Bundle savedInstanceState)
		{
			if (container == null)
			{
				// Currently in a layout without a container, so no reason to create our view.
				return null;
			}

			LinearLayout layout = new LinearLayout (Activity);
			layout.Orientation = Orientation.Vertical;

			var text = new TextView(Activity);
			text.TextSize = 32;
			text.Text = "Tap a word";
			layout.AddView (text);

			ListView list = new ListView (Activity);
			list.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, 
				Arguments.GetStringArray("spanish"));

			list.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs e) {
				text.Text = Arguments.GetStringArray ("english") [e.Position];
			};

			layout.AddView (list);
			return layout;
		}
	}
}

