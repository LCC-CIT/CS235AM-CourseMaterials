
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
	[Activity (Label = "SecondActivity")]			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.Second);
			var secondlabel = FindViewById<TextView> (Resource.Id.screen2Label);
			secondlabel.Text = Intent.GetStringExtra("MessageFromAct1")??"not available";

			var pokeScreen1 = FindViewById<Button>(Resource.Id.pokeScreen1);
			
			pokeScreen1.Click += (sender, e) => {
				var poke1 = new Intent(this, typeof(Activity1));
				poke1.PutExtra("MessageFromAct2","Screen 2 poked you");
				StartActivity (poke1);
			};
		}
	}
}

