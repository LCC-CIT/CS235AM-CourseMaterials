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
using System.IO;
using System.Collections;

namespace TideFragments
{
	internal  class Tidelist
	{
		List<Tide> tempTideList = new List<Tide> ();
		public List<Tide> myList{ get ; set; }


//		public Tidelist ()
//		{
//			Stream seedDataStream= Activity.Assets.Open (@"tidetable.txt");
//
//				System.Text.StringBuilder sb = new System.Text.StringBuilder ();
//				using (var reader = new StreamReader (seedDataStream)) {
//					string line;
//					string s = reader.ReadLine ();
//					string[] parts = s.Split ('\t');
//					while ((line = reader.ReadLine ()) != null) {
//						var delimiter = new char[] {
//							'\t'
//						};
//						var segments = line.Split (delimiter, StringSplitOptions.RemoveEmptyEntries);
//						//line of strings in segment
//						string date = Convert.ToString (segments [0]);
//						string day = Convert.ToString (segments [1]);
//						string time = Convert.ToString (segments [2]);
//						string ft = Convert.ToString (segments [3]);
//						string cm = Convert.ToString (segments [4]);
//						string tidetype = Convert.ToString (segments [5]);
//						//high or low
//						var tide = new Tide (date, day, time, ft, cm, tidetype);
//						tempTideList.Add (tide);
//						//tideList.Add (tide);
//						//Exclude duplicate dates 
//						IEnumerable<Tide> noDuplicateDates = tempTideList.Distinct (new TideComparer ());
//						myList = noDuplicateDates.ToList ();
//					}
//				}
//			} 


		}

	}


