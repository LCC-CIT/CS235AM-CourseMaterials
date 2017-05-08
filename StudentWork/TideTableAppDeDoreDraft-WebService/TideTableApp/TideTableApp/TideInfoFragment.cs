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
using DAL;

namespace TideTableApp
{
	internal class TideInfoFragment : Android.Support.V4.App.Fragment
	{
		public static TideInfoFragment NewInstance(int n)
		{
			var detailsFrag = new TideInfoFragment {Arguments = new Bundle()};
			detailsFrag.Arguments.PutInt("selected", n);
			return detailsFrag;
		}

		public int CurrentID
		{
			get { return Arguments.GetInt("selected", 0); }
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			if (container == null)
			{
				// Currently in a layout without a container, so no reason to create our view.
				return null;
			}
			var scroller = new ScrollView(Activity);
			var text = new TextView(Activity);
			var padding = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Activity.Resources.DisplayMetrics));
			text.SetPadding(padding, padding, padding, padding);
			text.TextSize = 24;
			//here we need to access the database with the key found with the Container
			text.Text = CurrentSelectionInfo.Data;
			scroller.AddView(text);
			return scroller;
		}
	}
}


