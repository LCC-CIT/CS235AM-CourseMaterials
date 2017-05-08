using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Data.Core.Orm;
using Xamarin.Data.Core.WebServices;

namespace Xamarin.Data.Droid.Activities
{
	
    [Activity(Label = "Sessions")]
	public class SessionsActivity : ListActivity
    {
		Adapters.SessionsAdapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.list_with_spinner);

			LoadDataAsync ();
		}

		private async Task LoadDataAsync(){
			await Downloader.DownloadSessionXmlAsync ();


			// At this point, the files are downloaded & saved locally
			var sessions = SessionsXmlParser.Instance.Sessions;

			// TODO: Step 10 - Android - Delete and save new data
            //await App.Database.DeleteSessionsAsync ();
            //await App.Database.SaveSessionsAsync (sessions);

            // TODO: Step 11 - Android - Load from database and display
            //var sessionFromDatabase = await App.Database.GetSessionsAsync();
            //adapter = new Adapters.SessionsAdapter(this, sessionFromDatabase);
            //ListView.Adapter = adapter;
		}
    }
}
