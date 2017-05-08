
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

namespace RpsDemo.DynamicFrag
{
	[Activity (Label = "TranslateActivity")]			
	public class TranslateActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Translate);

			FragmentTransaction ft = FragmentManager.BeginTransaction ();
			var frag = FragmentManager.FindFragmentById (Resource.Id.fragContainer); 
			if (frag!= null)
				ft.Remove (frag);  
			ft.Add (Resource.Id.fragContainer, new TextFrag());
			ft.Commit ();
					}
	}
}

