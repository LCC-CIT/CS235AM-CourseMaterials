using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace Lab2
{
	[Activity (MainLauncher = true)]
	public class MainActivity : Activity
	{
		List<Tide> Tides;
		List<Days> ListOfDays;
		ListView listView;	

		protected override void OnCreate (Bundle bundle)
		{

			SetContentView (Resource.Layout.HomeScreen);
			listView = FindViewById<ListView> (Resource.Id.DaysInMonth);
			bool dateExists = false;
			ListOfDays = new List<Days> ();
			Tides = new List<Tide> ();

			TextParser parser = new TextParser ("\t", 5);
			var TideList = parser.ParseText(Assets.Open(@"tides_oct.txt"));

			foreach (string[] tideInfo in TideList) {
				Tide t = new Tide () { Date = tideInfo[0], Day = tideInfo[1], Time = tideInfo[2], Height = tideInfo[3], hilo = tideInfo[4]  };
				foreach (var d in ListOfDays) {
					if (d.ToString () == t.Day + " " + t.Date) {
						d.Tides.Add (t);
						dateExists = true;
					}
				}
					if (!dateExists) {
						var tmpDay = new Days ();
						tmpDay.Tides.Add(t);
						ListOfDays.Add (tmpDay);
					}
					dateExists = false;
			}


			listView.Adapter = new ScreenAdapter (this, ListOfDays);
			listView.FastScrollEnabled = true;

			listView.ItemClick += OnListItemClick;

			base.OnCreate (bundle);
		}

		protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
		{
			var listView = sender as ListView;
			var d = ListOfDays [e.Position];
			string line = "";
			foreach (var tide in d.Tides) {
				string tmpH = tide.hilo == "H" ? "High" : "Low";
				line += tide.Time + " " + tide.Height + " " + tmpH + "\n";
			}

			FindViewById<TextView> (Resource.Id.TideInfo).Text = line;
		}
	}
}