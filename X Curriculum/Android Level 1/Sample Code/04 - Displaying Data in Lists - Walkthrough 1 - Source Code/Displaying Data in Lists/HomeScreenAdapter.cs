namespace BasicTable
{
    using System.Collections.Generic;

    using Android.App;
    using Android.Views;
    using Android.Widget;

    using Java.Lang;

    public class HomeScreenAdapter : BaseAdapter<Flora>, ISectionIndexer
    {
        private readonly Activity context;
        private readonly Flora[] items;
        private Dictionary<string, int> alphaIndex;

        // -- Code for the ISectionIndexer implementation follows --
        private string[] sections;
        private Object[] sectionsObjects;

        public HomeScreenAdapter(Activity context, Flora[] items)
        {
            this.context = context;
            this.items = items;
            BuildSectionIndex();
        }

        public override int Count { get { return items.Length; } }

        public override Flora this[int position] { get { return items[position]; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public int GetPositionForSection(int section)
        {
            return alphaIndex[sections[section]];
        }

        public int GetSectionForPosition(int position)
        {
            return 1;
        }

        public Object[] GetSections()
        {
            return sectionsObjects;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView; // re-use an existing view, if one is available
            if (view == null)
            {
                // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }
            var tv = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            tv.Text = items[position].Name;
            return view;
        }

        /// <summary>
        ///   This method will build the
        /// </summary>
        private void BuildSectionIndex()
        {
            alphaIndex = new Dictionary<string, int>();
            for (var i = 0; i < items.Length; i++)
            {
                // Use the first character in the heading as a key.
                var key = items[i].Name.Substring(0,1);
                if (!alphaIndex.ContainsKey(key))
                {
                    alphaIndex.Add(key, i);
                }
            }

            sections = new string[alphaIndex.Keys.Count];
            alphaIndex.Keys.CopyTo(sections, 0);
            sectionsObjects = new Object[sections.Length];
            for (var i = 0; i < sections.Length; i++)
            {
                sectionsObjects[i] = new String(sections[i]);
            }
        }
    }
}
