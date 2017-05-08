
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
	public class CircleFrag : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, 
			ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.CircleFrag, container, false);

			// Get the height and width entered by the user and convert the data type
			EditText radiusEditText = view.FindViewById<EditText> (Resource.Id.radiusEditText);

			Button button = view.FindViewById<Button> (Resource.Id.calculateButton);
			button.Click += delegate {
				double radius = double.Parse (radiusEditText.Text);
				AreaCalc calc = new AreaCalc();
				((MainActivity)this.Activity).DisplayResult(calc.calcCircleArea(radius));
			};
			return view;

		}

	}
}

