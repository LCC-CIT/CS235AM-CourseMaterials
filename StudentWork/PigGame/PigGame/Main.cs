using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PigGame
{
	[Activity( MainLauncher = true, Label = "@string/ApplicationName", Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar" )]
	public class Main : TabActivity
	{

		protected override void OnCreate( Bundle bundle )
		{
			base.OnCreate( bundle );

			// Set our view from the "main" layout resource
			SetContentView( Resource.Layout.Main );

			TabHost.TabSpec spec; // Resusable TabSpec for each tab
			Intent intent; // Reusable Intent for each tab

			// Create an Intent to launch an Activity for the tab (to be reused)
			intent = new Intent( this, typeof( Menu ) );
			intent.AddFlags( ActivityFlags.NewTask );

			// Initialize a TabSpec for each tab and add it to the TabHost
			spec = TabHost.NewTabSpec( "Menu" );
			spec.SetIndicator( "Menu", Resources.GetDrawable( Resource.Drawable.ic_tab_artists ) );
			spec.SetContent( intent );
			TabHost.AddTab( spec );

			// Game Tab
			intent = new Intent( this, typeof( Game ) );
			intent.AddFlags( ActivityFlags.NewTask );

			spec = TabHost.NewTabSpec( "Game" );
			spec.SetIndicator( "Game", Resources.GetDrawable
			( Resource.Drawable.ic_tab_artists ) );
			spec.SetContent( intent );
			TabHost.AddTab( spec );

			// Settings Tab
			intent = new Intent( this, typeof( Settings ) );
			intent.AddFlags( ActivityFlags.NewTask );

			spec = TabHost.NewTabSpec( "Settings" );
			spec.SetIndicator( "Settings", Resources.GetDrawable
			( Resource.Drawable.ic_tab_artists ) );
			spec.SetContent( intent );
			TabHost.AddTab( spec );

			TabHost.CurrentTab = 0;
		}

	}
}

