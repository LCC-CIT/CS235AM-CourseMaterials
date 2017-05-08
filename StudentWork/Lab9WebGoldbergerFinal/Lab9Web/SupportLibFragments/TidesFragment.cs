using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using System.Threading.Tasks;

namespace Lab7Fragments
{
    internal class TidesFragment : Fragment
    {
		public static Tide[] tides;
		public static Tide[] dates;
		public static TextView text;
		public static ListView list;
		public static Button btnClrDb;
		public static RelativeLayout layout;

		public static int selTide = DateTime.Now.DayOfYear-1;
		public static TidesActivityAdapter adapter;
		public static ProgressDialog progDialog;
		public static Tides tidesObj;

        public static TidesFragment NewInstance(string name, string id, string state, int selStat)
        {
            var tidesFrag = new TidesFragment {Arguments = new Bundle()};
			tidesFrag.Arguments.PutInt("selStat", selStat);
			tidesFrag.Arguments.PutString("name", name);
			tidesFrag.Arguments.PutString("id", id);
			tidesFrag.Arguments.PutString("state", state);
            return tidesFrag;
		}

		public int selStat
		{
			get { return Arguments.GetInt("selStat", 0);}
			set {}
		}
		public string name
        {
			get { return Arguments.GetString("name");}
			set {}
        }
		public string id
		{
			get { return Arguments.GetString("id");}
			set{}
		}
		public string state
		{
			get { return Arguments.GetString("state");}
			set {}
		}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                // Currently in a layout without a container, so no reason to create our view.
                return null;
            }
			// Create the layout for this Fragment
			layout = new RelativeLayout(Activity);
			RelativeLayout.LayoutParams bParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
			RelativeLayout.LayoutParams tParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
			RelativeLayout.LayoutParams lParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);

			// Create a button to clear the current tide database, so a user can reclaim space
			btnClrDb = new Button (Activity);
			btnClrDb.LayoutParameters = bParams;

			btnClrDb.Text = "Clear tide data for this station";
			btnClrDb.SetBackgroundColor (Android.Graphics.Color.Crimson);
			layout.AddView (btnClrDb);
			btnClrDb.Click += (sender, e) => {
				var dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), id+".db3");
				File.Delete(dbPath);
				layout.RemoveView(list);
			};

			// Create the text view to display selected location, selected date, and tide info
            text = new TextView(Activity);
			text.LayoutParameters = tParams;
			tParams.TopMargin = 50;

			var metrics = Resources.DisplayMetrics;
			var heightInDp = metrics.HeightPixels;
			int textViewHeight;
			if (heightInDp < 360) textViewHeight = 100;
			else textViewHeight = 150;

			text.SetHeight(textViewHeight);
			text.SetBackgroundColor(Android.Graphics.Color.LightGray);
			text.SetTextColor(Android.Graphics.Color.Black);
			text.SetPadding(10, 0, 10, 0);
			layout.AddView(text);

			// Create the listview to display the dates
			list = new ListView(Activity);
			lParams.TopMargin = textViewHeight+50;
			list.LayoutParameters = lParams;

			progDialog = ProgressDialog.Show (MainActivity.context, "Loading", name + ", " + state + " Tides", true, false);

			Task.Factory.StartNew (() => {
				tidesObj = new Tides();
				tides = tidesObj.LoadTideTables(name, id, state);
			}).ContinueWith (task  => {
				DisplayTides(layout);
			}, TaskScheduler.FromCurrentSynchronizationContext ());

			if (savedInstanceState != null)
			{
				selTide = savedInstanceState.GetInt("selTide", DateTime.Now.DayOfYear-1);
				name = savedInstanceState.GetString("name");
				id = savedInstanceState.GetString("id");
				state = savedInstanceState.GetString("state");
			}

            return layout;
        }
		private int ConvertPixelsToDp(float pixelValue)
		{
			var dp = (int) ((pixelValue)/Resources.DisplayMetrics.Density);
			return dp;
		}

		public override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			outState.PutInt("selTide", selTide);
			outState.PutString("name", name);
			outState.PutString("id", id);
			outState.PutString("state", state);
		}
		public override void OnResume()
		{
			base.OnResume();
			list.SetSelection(selTide);
		}
		private void DisplaySelectedTide()
		{
			if (selTide > -1 && tides.Length >1)
			{
				var date = dates[selTide];
				// look for position of date in tides array
				int index = 0;
				int tideCount = 0;
				for (int i = 0; i < tides.Length; i++)
				{
					if (date.data.Substring(0,10) == tides[i].data.Substring(0,10))
					{
						if (tideCount == 0) 
						{
							index = i; // found it
							tideCount++;
						} 
						else 
							tideCount++;
					}
				}
				
				// Get handle to TextView to display daily tide info.

				text.Text = name + ", " + state + "\n"; // Show the location.
				text.Text += tides[index].getDate() + "\n";
				// Display the tides for the date.
				for (int i = index; i < index+tideCount; i++)
				{
					text.Text += tides[i].getTime() + ", " + tides[i].getInfo() + ", " + tides[i].getHeight() + "\n";
				}
			}
		}
		private void DisplayTides(RelativeLayout layout)
		{
			if (progDialog != null) {
				progDialog.Dismiss ();
				progDialog = null;
			}
			if (tides.Length > 1) {
				dates = tidesObj.GetDates ();
				string[] strDates = new string[dates.Length]; 
				for (int i = 0; i < dates.Length; i++) {
					strDates [i] = dates [i].getDate ();
				}
				adapter = new TidesActivityAdapter(Activity, dates);

				list.Adapter = adapter;
				list.ChoiceMode = ChoiceMode.Single;
				list.FastScrollEnabled = true;
				list.SetSelection(selTide);
				layout.AddView (list);

				list.ItemClick += (sender, e) => 
				{
					for (int i = 0; i< list.ChildCount; i++)
					{
						list.GetChildAt(i).FindViewById<LinearLayout>(Resource.Id.Text).SetBackgroundColor(Android.Graphics.Color.Black);
					}
					selTide = e.Position;
					DisplaySelectedTide ();
					e.View.FindViewById<LinearLayout>(Resource.Id.Text).SetBackgroundColor(Android.Graphics.Color.Gray);
				};
			} 
			if (tides.Length == 1)
			{
				if (name.Substring(0,6) == "Unable")
					text.Text += "\nUnable to connect to NOAA at this time.";
				else
					text.Text += "\nUnable to get tide information for " + name + ", " + state +" at this time.";
				string[] strDates = new String[1];
				strDates [0] = tides [0].data;
				var adapter = new ArrayAdapter<String> (Activity, Android.Resource.Layout.SimpleListItemChecked, strDates);
				list.Adapter = adapter;
				layout.AddView (list);
			}
			DisplaySelectedTide();
		}
    }
}