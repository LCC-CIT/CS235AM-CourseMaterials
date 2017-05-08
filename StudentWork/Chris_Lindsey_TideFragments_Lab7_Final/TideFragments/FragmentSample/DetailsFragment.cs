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
using TideChart;

#if USE_SUPPORT
using Android.Support.V4.App;
using FragmentUsingSupport;
#else
using Android.App;
#endif 

namespace TideFragments
{
#if USE_SUPPORT
	internal class DetailsFragment : Android.Support.V4.App.Fragment
#else
	internal class DetailsFragment : Android.App.Fragment
#endif
	{
		private StationInfo station;
		public static DetailsFragment NewInstance(int DayID)
		{

			var detailsFrag = new DetailsFragment {Arguments = new Bundle()};
			detailsFrag.Arguments.PutInt("current_day_id", DayID);
			return detailsFrag;
		}

		public int ShownDayId
		{
			get { return Arguments.GetInt("current_day_id", 0); }
		} 

		public override View OnCreateView(LayoutInflater inflater, ViewGroup 
		                                  container, Bundle savedInstanceState)
		{
			station = (FragmentManager.FindFragmentById (Resource.Id.titles_fragment) as DaysFragment).TheStation;
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
			string TheTides = string.Empty;
			foreach (TideInfo tide in station.TheDays[ShownDayId].TheTides) 
			{
				TheTides += tide.ToString () + "\n";
			}

			text.Text = TheTides;

			scroller.AddView(text); 

			return scroller;
		}
	} 
}

