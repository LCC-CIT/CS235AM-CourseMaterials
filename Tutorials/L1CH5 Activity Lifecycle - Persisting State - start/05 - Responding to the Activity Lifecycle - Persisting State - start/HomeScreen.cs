namespace PersistStateWalkthrough
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Android.App;
    using Android.OS;
    using Android.Widget;

    using Object = Java.Lang.Object;

    [Activity(Label = "Persist State", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreen : Activity
    {
        private List<Flora> listOfFlora = new List<Flora>();
        private ListView listView;
        private TextView selectedFloraText;
		private int selectedPosition = -1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.HomeScreen);
            listView = FindViewById<ListView>(Resource.Id.List);
            selectedFloraText = FindViewById<TextView>(Resource.Id.selected_flora);

			var previousState = LastNonConfigurationInstance as HomeScreenState;
			if (previousState == null)
			{
				LoadFloraList ();
			}
			else
			{
				// Use the listOfFlora fromt he previous instance of this Activity
				listOfFlora =previousState.ListOfFlora;
			}

			listView.Adapter = new HomeScreenAdapter(this, listOfFlora);
			listView.ItemClick += OnListItemClick;
			
			// Persistence: Get saved position
			if (bundle != null)
			{
				selectedPosition = bundle.GetInt("listview_selecteditemposition", -1);
			}
        }
		
		// Persistence: Restore display to previous state
		protected override void OnResume()
		{
			base.OnResume ();

			DisplayLastSelectedFlora();

			// This will cause the list to scroll to the selected item.
			listView.SetSelection(selectedPosition);
		}


        protected void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
			/*
            var t = listOfFlora [e.Position];
            selectedFloraText.Visibility = Android.Views.ViewStates.Visible;
            selectedFloraText.Text = "Selected flora : " + t.Name;
            */
			selectedPosition = e.Position;
			DisplayLastSelectedFlora ();
        }

        private void LoadFloraList()
        {
            listOfFlora.Add(new Flora { Name = "Vegetables", ItemCount = "65 items", ImageResourceId = Resource.Drawable.Vegetables });
            listOfFlora.Add(new Flora { Name = "Fruits", ItemCount = "17 items", ImageResourceId = Resource.Drawable.Fruits });
            listOfFlora.Add(new Flora { Name = "Flower Buds", ItemCount = "5 items", ImageResourceId = Resource.Drawable.FlowerBuds });

            // Pretend that creating this list takes a long time.
            Thread.Sleep(2500);

            listOfFlora.Add(new Flora { Name = "Legumes", ItemCount = "33 items", ImageResourceId = Resource.Drawable.Legumes });
            listOfFlora.Add(new Flora { Name = "Bulbs", ItemCount = "18 items", ImageResourceId = Resource.Drawable.Bulbs });
            listOfFlora.Add(new Flora { Name = "Tubers", ItemCount = "43 items", ImageResourceId = Resource.Drawable.Tubers });
        }

		private void DisplayLastSelectedFlora() 
		{
			if (selectedPosition > -1)
			{
				var flora = listOfFlora [selectedPosition];
				selectedFloraText.Text = "Selected flora : " + flora.Name;
			}
		} 

		/*
		// Persistence: Just here for experimenting- put a breakpoint here
		protected override void OnStop ()
		{
			base.OnStop ();
		}
		*/

		// Persistence: Save state when there is a configuration change
		public override Java.Lang.Object OnRetainNonConfigurationInstance ()
		{
			var savedState = new HomeScreenState (listOfFlora);
			return savedState;
		} 

		// Persistence: Save state when the activity is paused(?) (not when stopped?)
		// This works for configuration changes too, but is slower
		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			outState.PutInt("listview_selecteditemposition", selectedPosition);

		}
    }
}
