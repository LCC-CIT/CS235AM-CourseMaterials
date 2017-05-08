using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolveLite {
	
    [Activity(Label = "Sessions")]
	public class SessionsActivity : ListActivity
    {
		SessionsAdapter adapter;
		ProgressDialog progressDialog;
		//List<Session> sessions;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			progressDialog = ProgressDialog.Show(this, "Loading", "Sessions", true, false);

			Task.Factory.StartNew(() => {
				Downloader.DownloadSessionXml() ;
				// At this point, the files are downloaded & saved locally
				var sessions = SessionsXmlParser.Instance.Sessions;
				// At this point, the files are parsed into these two local vars

				// TODO: DemoAndroid1: Delete and save new data
				EvolveApp.Database.DeleteSessions ();
				EvolveApp.Database.SaveSessions (sessions);
				// At this point, the data is in the database, ready to be queried!

			}).ContinueWith(task => {
				DisplaySessions();
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		void DisplaySessions()
		{
			if (progressDialog != null)
			{
				progressDialog.Dismiss();
				progressDialog = null;
			}
			// TODO: DemoAndroid1: Load from database and display
			var sessions = EvolveApp.Database.GetSessions ();
			adapter = new SessionsAdapter(this, sessions);
			ListView.Adapter = adapter;
		}

		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			var sessionTitle = adapter[position].Title;
			Toast.MakeText(this, "You selected the session " + sessionTitle + ".", ToastLength.Short).Show();
		}
    }
}
