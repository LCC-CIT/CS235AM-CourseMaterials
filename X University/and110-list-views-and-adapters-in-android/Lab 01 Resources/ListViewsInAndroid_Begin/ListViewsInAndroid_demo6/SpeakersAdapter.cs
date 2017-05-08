using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;
using ListViewsInAndroid.Model;

namespace ListViewsInAndroid
{
    public class SpeakersAdapter: BaseAdapter<Speaker>, ISectionIndexer
    {
        private readonly List<Speaker> data;
        private readonly Activity context;

        public SpeakersAdapter(Activity activity, IEnumerable<Speaker> speakers)
        {
            data = speakers.OrderBy(s => s.Name).ToList();
            context = activity;

            SetupIndex();
        }

        // -- ISectionIndexer --
        Java.Lang.Object[] sectionsObjects;
        IEnumerable<IGrouping<string,Speaker>> grouping;
        Dictionary<int, int> alphaIndex = new Dictionary<int, int>();

        /// <summary>
        /// Setup for ISectionIndexer
        /// </summary>
        void SetupIndex()
        {
            grouping = data.GroupBy(x => x.Name[0].ToString());
            sectionsObjects = grouping.Select(x => new Java.Lang.String(x.Key)).ToArray();

            int count = 0;
            for (var i = 0; i < grouping.Count(); i++) {
                alphaIndex.Add(i, count);
                count += grouping.ElementAt(i).Count();
            }
        }

        public int GetPositionForSection(int section)
        {
            return grouping.Take(section).Sum(x => x.Count());
        }

        public int GetSectionForPosition(int position)
        {
            return alphaIndex.Last(x => x.Value <= position).Key;
        }

        public Java.Lang.Object[] GetSections()
        {
            return sectionsObjects;
        }
        // -- END ISectionIndexer --

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Speaker this [int index] {
            get { return data[index]; }
        }

        public override int Count {
            get { return data.Count; }
        }

        /// <summary>
        /// CUSTOM ROW STYLE !!
        /// </summary>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null) {
                view = context.LayoutInflater.Inflate(Resource.Layout.speaker_row, null);
            }
			
            var speaker = data[position];
			
            var imageView = view.FindViewById<ImageView>(Resource.Id.headshotImageView);
            var headshot = GetHeadShot(speaker.HeadshotUrl);
            imageView.SetImageDrawable(headshot);
			
            var speakerNameView = view.FindViewById<TextView>(Resource.Id.speakerNameTextView);
            speakerNameView.Text = speaker.Name;
			
            var companyNameTextView = view.FindViewById<TextView>(Resource.Id.companyNameTextView);
            companyNameTextView.Text = speaker.Company;
			
            var twitterHandleView = view.FindViewById<TextView>(Resource.Id.twitterTextView);
            twitterHandleView.Text = "@" + speaker.TwitterHandle;
			
            return view;
        }

        private readonly Dictionary<string,WeakReference> _headshots = new Dictionary<string, WeakReference>();

        private Drawable GetHeadShot(string url)
        {
            Drawable headshotDrawable = null;
            WeakReference wr;

            if (_headshots.TryGetValue(url, out wr)) {
                headshotDrawable = wr.Target as Drawable;
                if (headshotDrawable != null)
                    return headshotDrawable;
                else {
                    _headshots.Remove(url);
                }
            }

            try {
                headshotDrawable = Drawable.CreateFromStream(context.Assets.Open(url), null);
                _headshots.Add(url, new WeakReference(headshotDrawable));
            } catch (Exception ex) {
                Log.Debug(GetType().FullName, "Error getting headshot for " + url + ", " + ex.ToString());
                headshotDrawable = null;
            }

            return headshotDrawable;
        }
    }
}

