
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HelloTabWidget
{
	[Activity (Label = "HelloTabWidget", MainLauncher = true)]
	public class MainActivity : TabActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			TabHost.TabSpec spec;	// Resusable TabSpec for each tab
			Intent intent; 			// Resusable intent fo reach tab
			
			// Create an intent to launch an Actiity for  the tab (to be reused)
			intent = new Intent(this, typeof (ArtistsActivity));
			intent.AddFlags (ActivityFlags.NewTask);
			
			// Initialize a TabSpec for each tab and add it to the TabHost
			spec = TabHost.NewTabSpec ("artists");
			spec.SetIndicator ("Artists", 
			                   Resources.GetDrawable(Resource.Drawable.ic_tab_artists));
			spec.SetContent(intent);
			TabHost.AddTab (spec);
			
			// Do the same for the other tabs
			intent = new Intent(this, typeof (AlbumsActivity));
			intent.AddFlags (ActivityFlags.NewTask);
			
			spec = TabHost.NewTabSpec ("albums");
			spec.SetIndicator ("Albums", Resources.GetDrawable(Resource.Drawable.ic_tab_artists));
			spec.SetContent(intent);
			TabHost.AddTab (spec);
			
			intent = new Intent(this, typeof (SongsActivity));
			intent.AddFlags (ActivityFlags.NewTask);
			
			spec = TabHost.NewTabSpec ("songs");
			spec.SetIndicator ("Songs", Resources.GetDrawable(Resource.Drawable.ic_tab_artists));
			spec.SetContent(intent);
			TabHost.AddTab (spec);
			
			TabHost.CurrentTab = 0;
		}
	}
}

