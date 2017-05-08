using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.Collections.Generic;

namespace DesignerWalkthrough
{
    [Activity (Label = "DesignerWalkthrough", MainLauncher = true)]
    public class Activity1 : Activity
    {

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            
            //var contactsAdapter = new ContactsAdapter (this);       
            var contactsListView = FindViewById<ListView> (Resource.Id.contactsListView);  

			contactsListView.Adapter = new SessionsAdapter(this, PopulateSessionData());
            //contactsListView.Adapter = contactsAdapter;
        }


	       protected List<Session> PopulateSessionData()
	       {
				return new List<Session> () {
					new Session {Title="Introduction to Mobile Development",Speaker="Bryan Costanich", Location="Ballroom", Begins=new DateTime(2013,4,14,9,0,0)},
					new Session {Title="Hello iOS",Speaker="Bryan Costanich", Location="Ballroom", Begins=new DateTime(2013,4,14,10,0,0)},
					new Session {Title="Hello Android",Speaker="Bryan Costanich", Location="Ballroom", Begins=new DateTime(2013,4,14,15,0,0)},
					new Session {Title="Cross-platform Navigation",Speaker="Bryan Costanich", Location="Ballroom",Begins=new DateTime(2013,4,15,9,0,0)},
					new Session {Title="Cross-platform Projects",Speaker="Bryan Costanich", Location="Ballroom",Begins=new DateTime(2013,4,15,9,0,0)},
					new Session {Title="Keynote (Day 1)",Speaker="Miguel de Icaza, Nat Friedman", Location="Ballroom",Begins=new DateTime(2013,4,16,9,0,0)},
					new Session {Title="Keynote (Day 2)",Speaker="Miguel de Icaza, Nat Friedman", Location="Ballroom",Begins=new DateTime(2013,4,17,9,0,0)},
				};
			}
    }

}


