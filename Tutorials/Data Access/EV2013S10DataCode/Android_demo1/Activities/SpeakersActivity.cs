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
		//List<Speaker> speakers;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			progressDialog = ProgressDialog.Show(this, "Loading", "Speakers", true, false);
			Task.Factory.StartNew(() => {

				Downloader.DownloadSpeakerJson();
				// At this point, the files are downloaded & saved locally
				var speakers = SpeakersJsonParser.Instance.Speakers;
				// At this point, the files are parsed into these two local vars

				// TODO: DemoAndroid1: Delete and save new data
				EvolveApp.Database.DeleteSpeakers ();
				EvolveApp.Database.SaveSpeakers (speakers);
				// At this point, the data is in the database, ready to be queried!

			}).ContinueWith(task => {
				DisplaySpeakers();
			}, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void DisplaySpeakers()
        {
			if (progressDialog != null)
			{
				progressDialog.Dismiss();
				progressDialog = null;
			}

			// TODO: DemoAndroid1: Load from database and display
			var speakers = EvolveApp.Database.GetSpeakers ();
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
