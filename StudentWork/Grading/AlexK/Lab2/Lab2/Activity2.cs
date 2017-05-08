
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

namespace Lab2
{
	[Activity (Label = "Activity2")]			
	public class Activity2 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.Activity2);
			Button button2 = FindViewById<Button> (Resource.Id.Activity2Button);

			TextView view2 = FindViewById<TextView> (Resource.Id.Activity2Label);
			view2.Text = Intent.GetStringExtra("Poke") ?? "Data not available";

			button2.Click += (sender, e) => {
				var intent2 = new Intent(this, typeof(Activity1));
				intent2.PutExtra("Poke", this.Resources.GetString(Resource.String.Button2PokeMessage));
				StartActivity(intent2);
			};

		}
	}
}

