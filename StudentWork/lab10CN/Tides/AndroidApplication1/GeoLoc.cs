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
using Xamarin.Geolocation;

namespace Application
{
	public class GeoLoc
	{
		Geolocator geolocator;
		
		public GeoLoc (Activity context)
		{
			Setup (context);
		}

		public void Setup(Activity context, int accur = 50)
		{
			if (this.geolocator == null)
			this.geolocator = new Geolocator (context) { DesiredAccuracy = accur };
		}

		public Position GetPosition (Activity context)
		{
			Setup(context);

			Position position = null;

			position = this.geolocator.GetPositionAsync (timeout: 10000).Result;

			return position;
		}

	}
}

