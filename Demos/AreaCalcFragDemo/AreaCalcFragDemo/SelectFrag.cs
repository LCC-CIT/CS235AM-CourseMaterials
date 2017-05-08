
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

namespace AreaCalcFragDemo
{
	public class SelectFrag : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, 
			ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.SelectFrag, container, false);
			return view;
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);

			Button rectangleButton = this.Activity.FindViewById<Button> (Resource.Id.rectangleButton);
			rectangleButton.Click += delegate {
				var ft = FragmentManager.BeginTransaction ();
				var fragment2 = FragmentManager.FindFragmentById (Resource.Id.fragContainer2); 
				if (fragment2 != null)
					ft.Remove (fragment2);  // 3 different fragments could be loaded here, so remove whatever is there
				ft.Add (Resource.Id.fragContainer2, new RectangleFrag ());
				ft.Commit ();
			};

			Button triangleButton = this.Activity.FindViewById<Button> (Resource.Id.triangleButton);
			triangleButton.Click += delegate {
				var ft = FragmentManager.BeginTransaction ();
				var fragment2 = FragmentManager.FindFragmentById (Resource.Id.fragContainer2); 
				if (fragment2 != null)
					ft.Remove (fragment2);  // 3 different fragments could be loaded here, so remove whatever is there
				ft.Add (Resource.Id.fragContainer2, new TriangleFrag ());
				ft.Commit ();
			};

			Button circleButton = this.Activity.FindViewById<Button> (Resource.Id.circleButton);
			circleButton.Click += delegate {
				var ft = FragmentManager.BeginTransaction ();
				var fragment2 = FragmentManager.FindFragmentById (Resource.Id.fragContainer2); 
				if (fragment2 != null)
					ft.Remove (fragment2);  // 3 different fragments could be loaded here, so remove whatever is there
				ft.Add (Resource.Id.fragContainer2, new CircleFrag ());
				ft.Commit ();
			};
		}
	}
}

