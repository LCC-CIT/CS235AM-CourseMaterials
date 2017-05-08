using System.Linq;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Xamarin.WebServicesAsync.Rest.Droid.Activities
 {
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher")]
	public class RestActivity : ListActivity {

		//TODO: Step 6 - Create an activity to consume the data
//		Adapters.ConceptPropertyAdapter adapter;
//
//		protected override void OnCreate(Bundle bundle)
//		{
//		    base.OnCreate(bundle);
//
//			SetContentView (Resource.Layout.list_with_spinner);
//
//			adapter = new Adapters.ConceptPropertyAdapter(this, Enumerable.Empty<Model.ConceptProperty>());
//			ListView.Adapter = adapter;
//
//		}

//		protected override void OnResume ()
//		{
//			base.OnResume ();
//
//			//TODO: Step 8 - Make the call to load the data
////			LoadDataAsync();
//		}
//
//		public override bool OnCreateOptionsMenu (IMenu menu)
//		{
//			MenuInflater.Inflate (Resource.Menu.primary_menu, menu);
//			return base.OnCreateOptionsMenu (menu);
//		}
//
//		public override bool OnMenuItemSelected (int featureId, IMenuItem item)
//		{
//			switch (item.ItemId) {
//				case Resource.Id.action_refresh:
//				LoadDataAsync ();
//					break;
//				default:
//					break;
//			}
//
//			return base.OnMenuItemSelected (featureId, item);
//		}

		//TODO: Step 7 - Call the services using async and update the UI with the results
//		private async Task LoadDataAsync(){
//			adapter.ConceptProperties.Clear ();
//			adapter.NotifyDataSetChanged ();
//
//			var restClient = new Client.RestClient ();
//
//			adapter.ConceptProperties.AddRange (await restClient.GetDataAsync ());
//			adapter.NotifyDataSetChanged ();
//		}
	}
}
