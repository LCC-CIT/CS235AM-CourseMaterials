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

namespace Flow
{
	public class TideAdapter : BaseAdapter<TideDays>, ISectionIndexer
	{
		List<TideDays> items;
		Activity context;

		public TideAdapter ( Activity c, List<TideDays> i )
			: base()
		{
			items = i;
			context = c;
			BuildSectionIndex();
		}

		public override long GetItemId( int position )
		{
			return position;
		}

		public override TideDays this [ int position ]
		{
			get { return items[position]; }
		}

		public override int Count
		{
			get { return items.Count; }
		}

		public override View GetView( int position, View convertView, ViewGroup parent )
		{
			View view = convertView;
			if ( view == null )
				view = context.LayoutInflater.Inflate( Android.Resource.Layout.TwoLineListItem, null );
			view.FindViewById<TextView>( Android.Resource.Id.Text1 ).Text = items[position].textReturn1();
			return view;
		}

		// -- Code for the ISectionIndexer implementation follows --
		String[] sections;
		Java.Lang.Object[] sectionsObjects;
		Dictionary<string, int> alphaIndex;

		public int GetPositionForSection( int section )
		{
			return alphaIndex[sections[section]];
		}

		public int GetSectionForPosition( int position )
		{
			return 1;
		}

		public Java.Lang.Object[] GetSections()
		{
			return sectionsObjects;
		}
		
		private void BuildSectionIndex()
		{
			alphaIndex = new Dictionary<string, int>();		// Map sequential numbers
			for ( var i = 0; i < items.Count; i++ )
			{
				// Use the part of speech as a key
				var key = items[i].date;
				if ( !alphaIndex.ContainsKey( key.ToString( "yyyy/MM/dd" ) ) )
				{
					alphaIndex.Add( key.ToString( "yyyy/MM/dd" ), i );
				}
			}

			sections = new string[alphaIndex.Keys.Count];
			alphaIndex.Keys.CopyTo( sections, 0 );
			sectionsObjects = new Java.Lang.Object[sections.Length];
			for ( var i = 0; i < sections.Length; i++ )
			{
				sectionsObjects[i] = new Java.Lang.String( sections[i] );
			}
		}
	}
}