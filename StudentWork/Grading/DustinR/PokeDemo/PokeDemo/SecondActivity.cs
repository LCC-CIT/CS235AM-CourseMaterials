
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

namespace MultiscreenDemo
{
	[Activity (Label = "Poke Demo")]			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Second);
			var label = FindViewById<TextView> (Resource.Id.screen2Label);
			label.Text = Intent.GetStringExtra("Poke") ?? "";

			var PokeScreen1 = FindViewById<Button> (Resource.Id.PokeScreen1);

			PokeScreen1.Click += delegate(object sender, EventArgs e) {
				var poke = new Intent(this, typeof(FirstActivity));
				poke.PutExtra("Poke", "Screen 2 poked you");
				StartActivity(poke);
			};
		
		}
	}
}


