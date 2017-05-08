using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.App;

#if USE_SUPPORT
using Android.Support.V4.App;
#endif

namespace L2CH2Fragments
{
#if USE_SUPPORT
	internal class DetailsFragment : Android.Support.V4.App.Fragment
#else
	internal class DetailsFragment : Fragment
#endif
	{
		/*
		// This is one way to create an instance of the DetailsFragment (The walkthrough does it this way)
		// Another way is just to create it in the DetailsActivity
		public static DetailsFragment NewInstance(int playId)
		{
			var detailsFrag = new DetailsFragment {Arguments = new Bundle()};
			detailsFrag.Arguments.PutInt("current_play_id", playId);
			return detailsFrag;
		}
		*/

		public int ShownPlayId
		{
			get { return Arguments.GetInt("current_play_id", 0); }
		} 
		
		public override View OnCreateView(LayoutInflater inflater, ViewGroup 
		                                  container, Bundle savedInstanceState)
		{
			if (container == null)
			{
				// Currently in a layout without a container, so no reason to 
				//create our view.
					return null;
			}

			var scroller = new ScrollView(Activity);

			var text = new TextView(Activity);
			var padding = 
				Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4,
				                                          Activity.Resources.DisplayMetrics)); 
			text.SetPadding(padding, padding, padding, padding);
			text.TextSize = 24;
			text.Text = Shakespeare.Dialogue[ShownPlayId];

			scroller.AddView(text); 

			return scroller;
		}
	} 
}

