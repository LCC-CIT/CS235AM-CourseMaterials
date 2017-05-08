using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloWorld
{
	[Activity (Label = "HelloWorld", MainLauncher = true)]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.Main); 
			
			var aButton = FindViewById<Button> (Resource.Id.aButton);
			var aLabel = FindViewById<TextView> (Resource.Id.helloLabel);  
			
			aButton.Click += (sender, e) => {
				aLabel.Text = "Hello from the button";
			};  
		}
	}
}


