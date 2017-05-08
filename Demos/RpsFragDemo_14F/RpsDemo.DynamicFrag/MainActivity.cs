using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RpsDemo.DynamicFrag
{
	[Activity (Label = "Rock, Paper, Scissors", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private bool isDualPane = false; // true for landscape orientation when two activities are loaded

		// The game object is a member of this class so that it can be accessed by this
		// Activity's fragment(s)
		private GameLogic game;

		public GameLogic Game
		{
			get {return game; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			game = new GameLogic ();

			// Detect the orientation: landscape or portrait
			// Only the dual pane layout (landscape) has fragContainer2
			View fragContainer2 = FindViewById<View> (Resource.Id.fragContainer2);
			if (fragContainer2 != null)
				isDualPane = true;

			// Load the fragment used by both layouts (landscape and portrait)
			// Note: The fragment has the ID of its container
			FragmentTransaction ft = FragmentManager.BeginTransaction ();
			var fragment1 = FragmentManager.FindFragmentById (Resource.Id.fragContainer1); 
			if (fragment1 != null)
				ft.Remove (fragment1);  
			var handFrag = new HandFrag ();
			ft.Add (Resource.Id.fragContainer1, handFrag);
			ft.Commit ();

			if (!isDualPane) {
				// Only the portrait layout (single pane) has a translate button
				Button translateButton = FindViewById<Button> (Resource.Id.translateButton);
				translateButton.Click += delegate(object sender, EventArgs e) {
					var intent = new Intent ();
					intent.SetClass (this, typeof(TranslateActivity));
					intent.PutExtra ("hand_position_name", game.HandName);
					StartActivity (intent);
				};
			} else {
				// Only add the Text fragment for landscape orientation (dual panes)
				ft = FragmentManager.BeginTransaction ();
				var fragment2 = FragmentManager.FindFragmentById (Resource.Id.fragContainer2);
				if (fragment2 != null)
					ft.Remove (fragment2);  
				ft.Add (Resource.Id.fragContainer2, new TextFrag ());
				ft.Commit ();
			}
		}	

		public void setTextFragText ()
		{
			if(isDualPane)
			{
				var text = FindViewById<TextView>(Resource.Id.handTextView);
				text.Text = game.HandName;
			}
		}
			
	}
}


