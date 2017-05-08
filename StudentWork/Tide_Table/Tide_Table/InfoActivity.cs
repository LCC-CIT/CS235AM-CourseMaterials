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

namespace Tide_Table
{
	[Activity (Label = "Tide Info")]			
	public class InfoActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var tideInfo = Intent.Extras.GetString ("tideInfo", "");

			var infos = InfoFragment.NewInstance (tideInfo);

			var ft = FragmentManager.BeginTransaction ();
			ft.Add (Android.Resource.Id.Content, infos);
			ft.Commit ();
		}
	}
}

