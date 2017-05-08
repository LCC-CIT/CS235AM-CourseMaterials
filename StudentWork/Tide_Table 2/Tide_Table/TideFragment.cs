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

namespace Tide_Table
{
	public class TideFragment : ListFragment
	{	
		public Tide[] tides;
		private string[] tideItemList;
		int selectedPosition = -1;
		bool _isDualPane;

		public override void OnCreate(Bundle bundle)
		{
			base.OnCreate (bundle);
			RetainInstance = true;
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);

			this.ListAdapter = new ArrayAdapter<String> (Activity, Android.Resource.Layout.SimpleListItemChecked, tideItemList);

			if (savedInstanceState != null)
				selectedPosition = savedInstanceState.GetInt ("selected", 0);

			var detailsFrame = Activity.FindViewById<View> (Resource.Id.details);
			_isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;
			if(_isDualPane)
			{
				ListView.ChoiceMode = ChoiceMode.Single;
				DisplayLastSelectedTide ();
			}
		}

	
		public override void OnAttach(Activity act)
		{
			base.OnAttach (act);
			var host = (TideActivity)Activity;
			tides = loadTideSheet ();
			tideItemList = new string[tides.Length];
			for(int i = 0; i < tides.Length; i++)
			{
				tideItemList[i] = (tides[i].Date + " " + tides[i].Day);
			}
		}

		public override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			outState.PutInt("selected", selectedPosition);
		}
		

		public void DisplayLastSelectedTide()
		{
			if (selectedPosition > -1)
			{

				string tideInfo = "";
				Tide tide = tides[selectedPosition];
		
				
				for (int i = 1; i < 5; i++) {
					tideInfo += tide.Tides [i];
					tideInfo += "\n";
				}


				if (_isDualPane) {
					ListView.SetItemChecked (selectedPosition, true);

					var info = FragmentManager.FindFragmentById (Resource.Id.details) as InfoFragment;
					if (info == null || info.ShownInfo != tideInfo) {
						info = InfoFragment.NewInstance (tideInfo);

						var ft = FragmentManager.BeginTransaction ();
						ft.Replace (Resource.Id.details, info);
						ft.SetTransition (FragmentTransit.FragmentFade);
						ft.Commit ();
					}
				}
				else
				{
					var intent = new Intent();

					intent.SetClass (Activity, typeof(InfoActivity));
					intent.PutExtra ("tideInfo", tideInfo);
					StartActivity (intent);
				}
			}
		}
		private Tide[] loadTideSheet()
		{
			var tides = new System.Collections.Generic.List<Tide>();
			var tideDescriptions = new System.Collections.Generic.List<String>();

			var seedDataStream = Activity.Assets.Open (@"TideChart.txt");
			using (var reader = new System.IO.StreamReader (seedDataStream))
			{
				char[] splitter = {'\t'};
				string[] line = reader.ReadLine ().Split (splitter, StringSplitOptions.RemoveEmptyEntries);
				while(!reader.EndOfStream)
				{
					//create new tide with date and day
					Tide tide = new Tide {Date = line[0], Day = line[1]};
					for(int i = 1; i < 5; i++)
					{
						if(reader.EndOfStream)
							break;

						if(line[5]=="H")
						{
							line[5] += "igh";
						}
						else
						{
							line[5] += "ow";
						}
						line[3] += " ft";
						line[4] += " cm";

						tide.Tides.Add (i, line[2] + " " + line[3] + " " + line[4] + " " + line[5]);
						line = reader.ReadLine ().Split (splitter, StringSplitOptions.RemoveEmptyEntries);
					}
					tides.Add (tide);

				}
			}

			return tides.ToArray ();
		}
		public override void OnListItemClick(ListView l, View v, int index, long id)
		{
			selectedPosition = index;
			DisplayLastSelectedTide ();
		}




	}
}

