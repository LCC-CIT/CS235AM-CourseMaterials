namespace FragmentSample
{
    using System;

    using Android.OS;
    using Android.Support.V4.App;
    using Android.Util;
    using Android.Views;
    using Android.Widget;

    internal class DetailsFragment : Fragment
    {
        public int ShownPlayId { get { return Arguments.GetInt("current_play_id", 0); } }

        public static DetailsFragment NewInstance(int playId)
        {
            DetailsFragment detailsFrag = new DetailsFragment { Arguments = new Bundle() };
            detailsFrag.Arguments.PutInt("current_play_id", playId);
            return detailsFrag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                // Currently in a layout without a container, so no reason to create our view.
                return null;
            }

            ScrollView scroller = new ScrollView(Activity);

            TextView text = new TextView(Activity);
            int padding = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Activity.Resources.DisplayMetrics));
            text.SetPadding(padding, padding, padding, padding);
            text.TextSize = 24;
            text.Text = Shakespeare.Dialogue[ShownPlayId];

            scroller.AddView(text);

            return scroller;
        }
    }
}
