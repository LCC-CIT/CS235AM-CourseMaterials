using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace EvolveLite {
	
    [Activity(Label = "Sessions")]
	public class SessionsActivity : ListActivity
    {
		SessionsAdapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			// TODO: Demo1c: Download web service data - blocks UI thread
			Downloader.DownloadSessionXml() ;
			DisplaySessions();
        }

		private void DisplaySessions()
		{
			// TODO: Demo1c: Deserialized and display XML
			var sessions = SessionsXmlParser.Instance.Sessions;
			
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
