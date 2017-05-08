using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

namespace EvolveLite
{
    public class SpeakersAdapter : BaseAdapter<Speaker>
    {
        private readonly Activity activity;
        private readonly List<Speaker> speakers;

        public SpeakersAdapter(Activity context, IEnumerable<Speaker> speakers)
        {
            this.speakers = speakers.ToList();
            activity = context;
        }

        public override int Count { get { return speakers.Count; } }

        public override Speaker this[int position] { get { return speakers[position]; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.speaker_row, null);

            var speaker = speakers[position];

//            var imageView = view.FindViewById<ImageView>(Resource.Id.headshotImageView);
//            var headshot = GetHeadShot(speaker.HeadshotUrl);
//            imageView.SetImageDrawable(headshot);

            var speakerNameView = view.FindViewById<TextView>(Resource.Id.speakerNameTextView);
            speakerNameView.Text = speaker.Name;

			var sessionCountView = view.FindViewById<TextView>(Resource.Id.sessionCountTextView);
			sessionCountView.Text = speaker.Company;

            return view;
        }

        private Drawable GetHeadShot(string url)
        {
            var headshotUrl = url.Remove(0, 1);
            Drawable headshotDrawable = null;
            try
            {
                headshotDrawable = Drawable.CreateFromStream(activity.Assets.Open(headshotUrl), null);
            }
            catch (Exception)
            {
                Console.WriteLine("No headshot for " + url);
                headshotDrawable = null;
            }
            return headshotDrawable;
        }
    }
}
