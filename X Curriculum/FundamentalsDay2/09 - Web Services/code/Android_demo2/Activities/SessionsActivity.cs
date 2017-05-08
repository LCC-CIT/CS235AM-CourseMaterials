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
		List<Session> sessions;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			progressDialog = ProgressDialog.Show(this, "Loading", "Sessions", true, false);
			// TODO: Demo2c: Download web service data - on background thread
			Task.Factory.StartNew(() => {
				Downloader.DownloadSessionXml() ;
				sessions = SessionsXmlParser.Instance.Sessions;
			}).ContinueWith(task => {
				DisplaySessions();
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		void DisplaySessions()
		{
			// TODO: Demo2c: dismiss the loading message
			if (progressDialog != null)
			{
				progressDialog.Dismiss();
				progressDialog = null;
			}
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
