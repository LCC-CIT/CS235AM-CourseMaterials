using Android.OS;
using Android.App;

#if USE_SUPPORT
using Android.Support.V4.App;
#endif

namespace TideFragments
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
            var index = Intent.Extras.GetInt("current_day_id", 0);

            var details = DetailsFragment.NewInstance(index); // Details
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
