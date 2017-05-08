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

namespace Tide_Table
{
	public class InfoFragment : Fragment
	{
		public static InfoFragment NewInstance(string tideInfo)
		{
			var tideInfoFrag = new InfoFragment { Arguments = new Bundle() };
			tideInfoFrag.Arguments.PutString ("tideInfo", tideInfo);
			return tideInfoFrag;
		}

		public string ShownInfo
		{
			get{return Arguments.GetString("tideInfo","");}
		}


		public override void OnCreate(Bundle bundle)
		{
			base.OnCreate (bundle);
			RetainInstance = true;
		}
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{


			if (container == null) {
				return null;
			}

			var text = new TextView (Activity);
			text.Text = ShownInfo;

			return text;
		}
	}
}

