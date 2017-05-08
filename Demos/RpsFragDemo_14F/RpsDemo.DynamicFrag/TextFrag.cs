
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace RpsDemo.DynamicFrag
{
	public class TextFrag : Fragment
	{

		// This is where you must inflate the fragment from it's axml layout
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.TextFrag, container, false);

			// Display the name of the hand position 
			// Note: we can't do this in the Activity's OnCreate, because the fragment
			// doesn't actually get added to the Activity's ViewGroup until after
			// OnCreate returns
			var name = Activity.Intent.GetStringExtra("hand_position_name");
			TextView nameTextView = view.FindViewById<TextView> (Resource.Id.handTextView);
			nameTextView.Text = name;

			return view;
		}


	}
}

