using System;
using System.IO;
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
using SQLite;
using DAL;

namespace TideTableApp
{
	public class TideListFragment : Android.Support.V4.App.ListFragment
	{
		private int selectedTide = 0;
		private bool isDualPane = false;

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			var data = (from tideData in DataBaseHandler.db.Table<TideData>() select tideData).ToList ();
			var dataN = data.Select (d => d.Date ).ToArray();
			var dataArray = dataN.ToArray ();

			var adapter = new ArrayAdapter<String>(Activity, Android.Resource.Layout.SimpleListItemChecked, dataArray);
			ListAdapter = adapter;
			if (savedInstanceState != null)
			{
				selectedTide = savedInstanceState.GetInt("selected", 0);
			}

			var infoFrame = Activity.FindViewById<View>(Resource.Id.tideInfo);
			isDualPane = infoFrame != null && infoFrame.Visibility == ViewStates.Visible;
			if (isDualPane)
			{
				ListView.ChoiceMode = ChoiceMode.Single;
				ShowInfo(selectedTide);
			}

			var dat = DataBaseHandler.db.Query<TideData> ("SELECT * FROM TD WHERE ID LIKE ?", selectedTide); 
			foreach (TideData t in dat) 
			{
				CurrentSelectionInfo.Data += t.Info;
			}

		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override void OnListItemClick(ListView l, View v, int position, long id)
		{
			CurrentSelectionInfo.Data = "";
			ShowInfo(position);
			var dat = DataBaseHandler.db.Query<TideData> ("SELECT * FROM TD WHERE ID LIKE ?", position); 
			foreach (TideData t in dat) 
			{
				CurrentSelectionInfo.Data += t.Info;
			}
		}
		private void ShowInfo(int n)
		{
			selectedTide = n;
			if (isDualPane)
			{
				//LARGE SCREEN solution. A fragment is added to the current activity
				ListView.SetItemChecked(selectedTide, true);

				var info = FragmentManager.FindFragmentById(Resource.Id.tideInfo) as TideInfoFragment;
				if (info == null || info.CurrentID != selectedTide)
				{
					// Make new fragment to show this selection.
					info = TideInfoFragment.NewInstance(selectedTide);
					// Execute a transaction, replacing any existing
					// fragment with this one inside the frame.
					var ft = FragmentManager.BeginTransaction();
					ft.Replace(Resource.Id.tideInfo, info);
					ft.Commit();
				}
			}
			else
			{
				//here is a SMALL SCREEN dealio. A new activity is created to show the data
				var intent = new Intent();
				intent.SetClass(Activity, typeof (TideInfoActivity));
				intent.PutExtra("selected", selectedTide);
				StartActivity(intent);
			}
		}
	}
}


