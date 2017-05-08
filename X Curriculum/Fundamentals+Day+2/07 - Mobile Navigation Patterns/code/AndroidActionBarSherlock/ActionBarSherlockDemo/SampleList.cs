/*
 * Copyright (C) 2011 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using ActionbarSherlock.App;
using ActionbarSherlock.View;

using IMenu = global::ActionbarSherlock.View.IMenu;
using IMenuItem = global::ActionbarSherlock.View.IMenuItem;
using MenuItem = global::ActionbarSherlock.View.MenuItem;
using ISubMenu = global::ActionbarSherlock.View.ISubMenu;

namespace Mono.ActionbarsherlockTest
{
	static class Constants
	{
		public const string DemoCategory = "mono.actionbarsherlocktest.EXAMPLE";
	}

	[Activity (Label = "ActionBarSherlock", Icon = "@drawable/ic_launcher", Theme = "@style/Theme.Sherlock")]
	[IntentFilter (new string [] { Intent.ActionMain },
		Categories = new string [] { Intent.CategoryLauncher, Intent.CategoryDefault })]
	public class SampleList : SherlockListActivity
	{
		public static int THEME = Resource.Style.Theme_Sherlock;
	
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			String path = Intent.GetStringExtra ("com.example.android.apis.Path");
		
			if (path == null) {
				path = "";
			}
		
			ListAdapter = new SimpleAdapter (this, GetData (path),
				Android.Resource.Layout.SimpleListItem1, new String[] { "title" },
				new int[] { Android.Resource.Id.Text1 });
			ListView.TextFilterEnabled = true;
		}
	
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			ISubMenu sub = menu.AddSubMenu ("Evolve");
			// group id, , order, text
			//sub.Add (0, 1, 1, "What's On");
			sub.Add (0, 2, 2, "Evolve Speakers");
			sub.Add (0, 3, 3, "Evolve Sessions");
			//sub.Add (0, 4, 5, "My Schedule");


//			sub.Add (1, Resource.Style.Theme_Sherlock, 4, "Default");
//			sub.Add (2, Resource.Style.Theme_Sherlock_Light, 5, "Light");
//			sub.Add (3, Resource.Style.Theme_Sherlock_Light_DarkActionBar, 6, "Light (Dark Action Bar)");
			sub.Item.SetShowAsAction (MenuItem.ShowAsActionAlways | MenuItem.ShowAsActionWithText);
			return true;

		}
	
		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home || item.ItemId == 0) {
				return false;
			} 
			var intent = new Intent();
			switch (item.Order) {
			case 1:
//				intent.SetClass(this, typeof(HomeActivity));
//				intent.AddFlags(ActivityFlags.ClearTop);            // http://developer.android.com/reference/android/content/Intent.html#FLAG_ACTIVITY_CLEAR_TOP
//				StartActivity(intent);
				break;
			case 2:
				intent.SetClass(this, typeof(SpeakersActivity));
				intent.AddFlags(ActivityFlags.ClearTop);            // http://developer.android.com/reference/android/content/Intent.html#FLAG_ACTIVITY_CLEAR_TOP
				StartActivity(intent);
				break;
			case 3:
				intent.SetClass(this, typeof(SessionsActivity));
				intent.AddFlags(ActivityFlags.ClearTop);            // http://developer.android.com/reference/android/content/Intent.html#FLAG_ACTIVITY_CLEAR_TOP
				StartActivity(intent);
				break;
			case 4:
//				intent.SetClass(this, typeof(HomeActivity));
//				intent.AddFlags(ActivityFlags.ClearTop);            // http://developer.android.com/reference/android/content/Intent.html#FLAG_ACTIVITY_CLEAR_TOP
//				StartActivity(intent);
				break;
			}

//			THEME = item.ItemId;
//			Toast.MakeText (this, "Theme changed to \"" + item.TitleFormatted + "\"", ToastLength.Short).Show ();
			return true;
		}
	
		protected IList<IDictionary<string,object>> GetData (string prefix)
		{
			List<IDictionary<string, object>> myData = new List<IDictionary<string, object>> ();
		
			Intent mainIntent = new Intent (Intent.ActionMain, null);
			mainIntent.AddCategory (Constants.DemoCategory);
		
			PackageManager pm = PackageManager;
			var list = pm.QueryIntentActivities (mainIntent, 0);
		
			if (null == list)
				return myData;
		
			String[] prefixPath;
			String prefixWithSlash = prefix;
		
			if (prefix == "") {
				prefixPath = null;
			} else {
				prefixPath = prefix.Split ('/');
				prefixWithSlash = prefix + "/";
			}
		
			int len = list.Count;
		
			IDictionary<String, Boolean> entries = new Dictionary<String, Boolean> ();
		
			for (int i = 0; i < len; i++) {
				ResolveInfo info = list [i];
				var labelSeq = info.LoadLabel (pm);
				String label = labelSeq != null
				? labelSeq.ToString ()
					: info.ActivityInfo.Name;
			
				if (prefixWithSlash.Length == 0 || label.StartsWith (prefixWithSlash)) {
				
					String[] labelPath = label.Split ('/');
				
					String nextLabel = prefixPath == null ? labelPath [0] : labelPath [prefixPath.Length];
				
					if ((prefixPath != null ? prefixPath.Length : 0) == labelPath.Length - 1) {
						AddItem (myData, nextLabel, ActivityIntent (
						info.ActivityInfo.ApplicationInfo.PackageName,
						info.ActivityInfo.Name));
					} else {
						if (entries.ContainsKey (nextLabel) == null) {
							AddItem (myData, nextLabel, BrowseIntent (prefix == "" ? nextLabel : prefix + "/" + nextLabel));
							entries [nextLabel] = true;
						}
					}
				}
			}
		
			myData.Sort (sDisplayNameComparator);
		
			return myData;
		}
	
		private readonly static Comparison<IDictionary<string, object>> sDisplayNameComparator = (map1, map2) => {
				return string.Compare (ToString (map1 ["title"]), ToString (map2 ["title"]));
			};

		static string ToString (Object obj)
		{
			return obj != null ? obj.ToString () : null;
		}
	
		protected Intent ActivityIntent (string pkg, string componentName)
		{
			Intent result = new Intent ();
			result.SetClassName (pkg, componentName);
			return result;
		}
	
		protected Intent BrowseIntent (string path)
		{
			Intent result = new Intent ();
			result.SetClass (this, typeof(SampleList));
			result.PutExtra ("com.example.android.apis.Path", path);
			return result;
		}
	
		protected void AddItem (IList<IDictionary<String, Object>> data, string name, Intent intent)
		{
			var temp = new JavaDictionary<string, Object> ();
			temp ["title"] = name;
			temp ["intent"] = intent;
			data.Add (temp);
		}
	
		//	@SuppressWarnings("unchecked")
		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			var map = (IDictionary<String, Object>)l.GetItemAtPosition (position);
			
			Intent intent = (Intent)map ["intent"];
			StartActivity (intent);
		}
	}
}
