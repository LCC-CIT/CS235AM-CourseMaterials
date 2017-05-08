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

namespace TidesDatabase
{
	class geoLocation
	{
		public double latitude { get; set; }
		public double longitude { get; set; }
		public string station_name { get; set; }
		public uint stationID { get; set; }

		public geoLocation ( string name, uint ID, double lat, double lon )
		{
			station_name = name;
			stationID = ID;
			latitude = lat;
			longitude = lon;
		}

		/// <summary>
		/// Input comparing coordinates
		/// <para>Returns true if Location1 is closer</para>
		/// <para>Returns false if Location2 is closer</para>
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		static public int compare ( geoLocation start, geoLocation [] list )
		{
			double [] x = new double [ list.Length ];
			double [] y = new double [ list.Length ];
			int closest = new int();
			double dist = new double();

			for(int i = 0; i < list.Length; i++)
			{
				x[i] = Math.Pow( start.latitude - list [ i ].latitude, 2 );
				y[i] = Math.Pow( start.longitude - list [ i ].longitude, 2 );
			}

			dist = x [ 0 ] + y [ 0 ];

			for(int i = list.Length - 1; i >= 0; i--)
			{
				if ( dist > x [ i ] + y [ i ] )
				{
					dist = x [ i ] + y [ i ];
					closest = i;
				}
			}

			return closest;
		}
	}
}