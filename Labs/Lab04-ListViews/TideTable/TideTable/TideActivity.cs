/* Written by Brian Bird
 * 4/30/2013
 * CS235M, LCC
 * Demo of using a ListView with a custom List Adapter
 */

using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TideTable
{
	[Activity (Label = "TideTable", MainLauncher = true)]
	public class TideActivity : Activity
	{
		List<Tide> tides;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//View tideListView = this.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			SetContentView(Resource.Layout.Main);

			var tideList = FindViewById<ListView>(Resource.Id.tideList);
			//tideList.Adapter = new TideAdapter(this, Android.Resource.Layout.SimpleListItem1, LoadTideTableFromAssests() );
			LoadTideTableFromAssests();
			tideList.Adapter = new TideAdapter(this, tides.ToArray () );
			tideList.FastScrollEnabled = true;

			tideList.ItemClick += (sender, e) =>
			{
				string tideHeight = tides[e.Position].GetHeight();
				Android.Widget.Toast.MakeText(this, tideHeight, Android.Widget.ToastLength.Short).Show();
			};
		}

		private void LoadTideTableFromAssests()
		{
			tides = new List<Tide>();
			Stream tideDataStream = Assets.Open (@"TideChart2013.txt");
			using (var reader = new StreamReader(tideDataStream))
			{
				while(!reader.EndOfStream)
				{
					string tide = reader.ReadLine();
					String[] tideInfo;
					if (tide[0] == '2')		//TODO find a better way to look for valid lines
					{
						tideInfo = tide.Split (new char[] {'\t'} );
						tides.Add(new Tide {DateAndTime = DateTime.Parse (tideInfo[0] + " " + tideInfo[2]), 
						                Height = float.Parse (tideInfo[3]), 
										High = tideInfo[7] == "H"});
					}
				}
			}
		}

	}
}


