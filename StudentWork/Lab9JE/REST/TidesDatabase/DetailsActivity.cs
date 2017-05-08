using Android.OS;
using Android.App;

#if USE_SUPPORT
using Android.Support.V4.App;
#endif

namespace Flow
{
    [Activity(Label = "Details Activity")]
#if USE_SUPPORT
	public class DetailsActivity : FragmentActivity
#else
    public class DetailsActivity : Activity
#endif
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var index = Intent.Extras.GetInt("current_tide_id", 0);
			var data = Intent.Extras.GetString("tide_data");
            var details = DetailsFragment.NewInstance(index, data); // Details
#if USE_SUPPORT
			var fragmentTransaction = SupportFragmentManager.BeginTransaction();
#else
			var fragmentTransaction = FragmentManager.BeginTransaction(); 
#endif            
            fragmentTransaction.Add(Android.Resource.Id.Content, details);
            fragmentTransaction.Commit();
        }
    }
}
