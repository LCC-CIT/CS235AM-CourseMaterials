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

namespace Flow
{
#if USE_SUPPORT
	internal class DetailsFragment : Android.Support.V4.App.Fragment
#else
	internal class DetailsFragment : Android.App.Fragment
#endif
	{
		public static DetailsFragment NewInstance(int tideId, string data)
		{
			var detailsFrag = new DetailsFragment {Arguments = new Bundle()};
			detailsFrag.Arguments.PutInt("current_tide_id", tideId);
			detailsFrag.Arguments.PutString("tide_data", data );
			return detailsFrag;
		}

		public int ShownTideId
		{
			get { return Arguments.GetInt("current_tide_id", 0); }
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
			text.Text = Arguments.GetString("tide_data");

			scroller.AddView(text); 

			return scroller;
		}
	} 
}

