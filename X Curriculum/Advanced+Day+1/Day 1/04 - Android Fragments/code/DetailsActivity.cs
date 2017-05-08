namespace FragmentSample
{
    using Android.App;
    using Android.OS;
    using Android.Support.V4.App;

    using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

    [Activity(Label = "Details Activity")]
    public class DetailsActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            int index = Intent.Extras.GetInt("current_play_id", 0);

            DetailsFragment details = DetailsFragment.NewInstance(index);
            FragmentTransaction fragmentTransaction = SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Add(Android.Resource.Id.Content, details, "MyFragTag");
            fragmentTransaction.Commit();
        }
    }
}
