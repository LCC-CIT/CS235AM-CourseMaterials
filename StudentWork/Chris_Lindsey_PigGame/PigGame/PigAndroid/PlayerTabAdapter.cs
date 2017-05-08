
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

using PigGame;

namespace PigAndroid
{
	class PlayerTabAdapter : BaseAdapter<RollInfo>
	{

		RollInfo[] theRolls;
		Activity Context;

		public PlayerTabAdapter(Activity context, RollInfo[] rolls) : base()
		{
			Context = context;
			theRolls = rolls;
		}

		public void UpdateTheRolls(RollInfo[] newRolls)
		{
			theRolls = newRolls;
		}

#region BaseAdapter override functions
		public override long GetItemId(int position)
		{
			return position;
		}
		
		public override RollInfo this[int position]
		{
			get {return theRolls[position];}
		}
		
		public override int Count 
		{
			get {return theRolls.Length;}
		}
		
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
			{
				view = Context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);
			}

			TextView text = view.FindViewById<TextView>(Android.Resource.Id.Text1);
			text.SetTypeface( Android.Graphics.Typeface.Monospace, Android.Graphics.TypefaceStyle.Normal);
			text.Text = theRolls[position].ToString();

			ImageView icon = view.FindViewById<ImageView>(Android.Resource.Id.Icon);
			switch (theRolls[position].Roll)
			{
			case 1:
				icon.SetImageResource(Resource.Drawable.Die1);
				break;
			case 2:
				icon.SetImageResource(Resource.Drawable.Die2);
				break;
			case 3:
				icon.SetImageResource(Resource.Drawable.Die3);
				break;
			case 4:
				icon.SetImageResource(Resource.Drawable.Die4);
				break;
			case 5:
				icon.SetImageResource(Resource.Drawable.Die5);
				break;
			case 6:
				icon.SetImageResource(Resource.Drawable.Die6);
				break;
			}

			return view;
		}
#endregion
	}
}

