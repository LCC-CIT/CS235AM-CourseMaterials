using System.Linq;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Xamarin.WebServicesAsync.Soap.Droid.Activities
 {
	
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher")]
	public class SoapActivity : ListActivity
    {
		//TODO: Step 4 - Create an activity to consume the data
		Adapters.ConceptPropertyAdapter adapter;

		protected override void OnCreate(Bundle bundle)
		{
		    base.OnCreate(bundle);

			//Set our activity's layout
			SetContentView (Resource.Layout.list_with_spinner);

			//Assign an adapter
			adapter = new Adapters.ConceptPropertyAdapter(this, Enumerable.Empty<Model.ConceptProperty>());
			ListView.Adapter = adapter;
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			//TODO: Step 6 - Make the call to load the data
			LoadDataAsync ();
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.primary_menu, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnMenuItemSelected (int featureId, IMenuItem item)
		{
			switch (item.ItemId) {
				case Resource.Id.action_refresh:
					LoadDataAsync ();
					break;
				default:
					break;
			}

			return base.OnMenuItemSelected (featureId, item);
		}
		
		//TODO: Step 5 - Call the services using async and update the UI with the results
		public async Task LoadDataAsync(){
			//Remove any items which may be in the list
			adapter.ConceptProperties.Clear ();
            adapter.NotifyDataSetChanged();

			//Create a soap client
			var soapClient = new Client.SoapClient ();
			//Query
			var foundConceptProperties = await soapClient.GetDataAsync ();

			//Assign response to our adapter and notify of updates
		 	adapter.ConceptProperties.AddRange (foundConceptProperties);
			adapter.NotifyDataSetChanged ();
		}
    }
}
