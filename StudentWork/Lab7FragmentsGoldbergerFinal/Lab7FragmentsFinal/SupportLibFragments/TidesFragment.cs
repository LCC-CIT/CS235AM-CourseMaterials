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


namespace Lab7Fragments
{
    internal class TidesFragment : Fragment
    {
		public static Tide[] tides;
		public static Tide[] dates;
		public TextView text;
		public ListView list;

		public static int selTide = 0;

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
			get { return Arguments.GetInt("selStat", -1);}
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
			var layout = new RelativeLayout(Activity);
			RelativeLayout.LayoutParams tParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
			RelativeLayout.LayoutParams lParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);

			// Create the text view to display selected location, selected date, and tide info
            text = new TextView(Activity);
			text.LayoutParameters = tParams;

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
			lParams.TopMargin = textViewHeight;
			list.LayoutParameters = lParams;

			var tidesObj = new Tides();
			tides = tidesObj.LoadTideTables(name, id, state);
			if (tides.Length > 1) 
			{
				dates = tidesObj.GetDates();
				string[] strDates = new string[dates.Length]; 
				for (int i = 0; i < dates.Length; i++)
				{
					strDates[i] = dates[i].getDate();
				}
				var adapter = new ArrayAdapter<String>(Activity, Android.Resource.Layout.SimpleListItemChecked, strDates);
		
				list.Adapter = adapter;
				list.ChoiceMode = ChoiceMode.Single;
				list.FastScrollEnabled = true;
				layout.AddView(list);

				list.ItemClick += (sender, e) => 
				{
					selTide = e.Position;
					DisplaySelectedTide();
				};
			}

			if (savedInstanceState != null)
			{
				selTide = savedInstanceState.GetInt("selTide", 0);
				name = savedInstanceState.GetString("name");
				id = savedInstanceState.GetString("id");
				state = savedInstanceState.GetString("state");
			}
			list.SetItemChecked(selTide, true);
			DisplaySelectedTide();

			if (tides.Length == 1) 
				text.Text += "\nUnable to get information on this station at this time";

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
				for (int i = 0; i < tides.Length; i++)
				{
					if (date == tides[i])
					{
						index = i; // found it
						break;
					}
				}
				
				// Get handle to TextView to display daily tide info.

				text.Text = name + ", " + state + "\n"; // Show the location.
				text.Text += tides[index].getDate() + "\n";
				// Display the four tides for the date.
				for (int i = index; i < index+4; i++)
				{
					text.Text += tides[i].getTime() + ", " + tides[i].getInfo() + ", " + tides[i].getHeight() + "\n";
				}
			}
		}
    }
}