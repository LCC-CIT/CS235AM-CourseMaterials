using System.Linq;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace EvolveLite {
    [Activity(Label = "@string/activity_speakers_label")]
    public class SpeakersActivity : ListActivity {
        SpeakersAdapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			// TODO: Demo1d: Download web service data - blocks UI thread
			Downloader.DownloadSpeakerJson();
			DisplaySpeakers();
        }

        private void DisplaySpeakers()
        {
			// TODO: Demo1d: Deserialized and display JSON
			var speakers = SpeakersJsonParser.Instance.Speakers;

            adapter = new SpeakersAdapter(this, speakers);
            ListView.Adapter = adapter;
        }

		
		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			var speakerName = adapter[position].Name;
			Toast.MakeText(this, "You selected the speaker " + speakerName + ".", ToastLength.Short).Show();
		}
	}
}
