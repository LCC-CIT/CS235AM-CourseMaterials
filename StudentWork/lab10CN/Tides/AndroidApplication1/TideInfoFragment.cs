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
using Android.Support.V4.App;

namespace Application
{
	public class TideInfoFragment : Fragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public static TideInfoFragment NewInstance(int dayID)
		{
			var infoFrag = new TideInfoFragment { Arguments = new Bundle () };
			return infoFrag;
		}
	}
}

