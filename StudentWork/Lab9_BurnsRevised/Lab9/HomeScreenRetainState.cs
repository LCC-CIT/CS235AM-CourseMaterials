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

namespace Lab9
{
	public class HomeScreenRetainState : Java.Lang.Object
	{
		public List<Tide> listofTides { get; private set; }
		public int selectedPos;
		
		public HomeScreenRetainState (List<Tide> list, int selected)
		{
			listofTides = list;
			selectedPos = selected;
		}
		
	}
}

