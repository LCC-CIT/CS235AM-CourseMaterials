using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;

namespace BasicTable
{
	[Activity (Label = "BasicTable", MainLauncher = true, Icon = "@drawable/icon")] 
	public class HomeScreen : ListActivity
	{
		Flora[] items;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//items = new string[] { "Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers" };
			//ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);

			/*
			items = new Flora[] {
				new Flora { Name = "Vegetables"},
				new Flora { Name = "Fruits"},
				new Flora { Name = "Flower Buds"},
				new Flora { Name = "Legumes"},
				new Flora { Name = "Bulbs"},
				new Flora { Name = "Tubers"}
			};
			*/

			items = LoadListOfVegetablesFromAssets();
			ListAdapter = new HomeScreenAdapter(this, items);
			ListView.FastScrollEnabled = true;
			}
		
		protected override void OnListItemClick(ListView l, View v, int position, long id) 
		{
			var vegetable = items[position];
			Android.Widget.Toast.MakeText(this, vegetable.ToString(), Android.Widget.ToastLength.Short).Show();
		}

		/// <summary>
		///   Loads the list of vegetables from the text file VegeData.txt which is an Android Asset.
		/// </summary>
		/// <returns> A sorted list of vegetables. </returns>
		private Flora[] LoadListOfVegetablesFromAssets()
		{
			var veges = new List<Flora>();
			var seedDataStream = Assets.Open(@"VegeData.txt");
			using (var reader = new StreamReader(seedDataStream))
			{
				while (!reader.EndOfStream)
				{
					var flora = new Flora { Name = reader.ReadLine() }; 
					veges.Add(flora);
				}
			}
			veges.Sort((x, y) => String.Compare(x.Name, y.Name, 
			                                    StringComparison.Ordinal));
			items = veges.ToArray();
			
			return items;
		} 
		
	}



	public class HomeScreenAdapter : BaseAdapter<Flora>, ISectionIndexer
	{
		Flora[] items;
		Activity context;

		public HomeScreenAdapter(Activity context, Flora[] items) //: base() 
		{
			this.context = context; 
			this.items = items;
			BuildSectionIndex();
		}

		public override long GetItemId(int position)
		{ 
			return position;
		}

		public override Flora this[int position] 
		{   
			get { return items[position]; } 
		}

		public override int Count 
		{
			get { return items.Length; } 
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
				if (view == null) // otherwise create a new one
					view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null); 
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position].ToString();
			return view;
		}

		// -- Code for the ISectionIndexer implementation follows --
		String[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex; 

		public int GetPositionForSection(int section)
		{
			return alphaIndex [sections [section]];
		}
		
		public int GetSectionForPosition(int position)
		{
			return 1;
		}

		public Java.Lang.Object[] GetSections()
		{
			return sectionsObjects;
		}

		private void BuildSectionIndex()
		{
			alphaIndex = new Dictionary<string, int>();
			for (var i = 0; i < items.Length; i++)
			{
				// Use the first character in the name as a key.
				var key = items[i].Name.Substring(0,1);
				if (!alphaIndex.ContainsKey(key))
				{
					alphaIndex.Add(key, i);
				} 
			}
			
			sections = new string[alphaIndex.Keys.Count];
			alphaIndex.Keys.CopyTo(sections, 0);
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for (var i = 0; i < sections.Length; i++)
			{
				sectionsObjects[i] = new Java.Lang.String(sections[i]);
			}
		} 
		
	} 

	/// <summary>
	///   A simple class for holding the data that is read from VegeData.txt
	/// </summary>
	public class Flora
	{
		public string Name { get; set; }
		public override string ToString() 
		{
			return Name;
		}
	} 
}


