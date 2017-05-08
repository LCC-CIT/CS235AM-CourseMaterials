using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace EvolveLite {
    [Activity(Label = "@string/activity_speakers_label")]
    public class SpeakersActivity : ListActivity {
        
		SpeakersAdapter adapter;
		ProgressDialog progressDialog;
		List<Speaker> speakers;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			progressDialog = ProgressDialog.Show(this, "Loading", "Speakers", true, false);
			// TODO: Demo2d: Download web service data - on background thread
			Task.Factory.StartNew(() => {
				Downloader.DownloadSpeakerJson();
				speakers = SpeakersJsonParser.Instance.Speakers;
			}).ContinueWith(task => {
				DisplaySpeakers();
			}, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void DisplaySpeakers()
        {
			// TODO: Demo2d: dismiss the loading message
			if (progressDialog != null)
			{
				progressDialog.Dismiss();
				progressDialog = null;
			}
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
