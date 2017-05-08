
using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MultiScreen
{
	[Activity (Label = "MultiScreen", MainLauncher = true)]
	public class FirstActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			SetContentView (Resource.Layout.Main);
			bool pop = false;
			int count = Intent.GetIntExtra ("Progress", 0);
			pop = Intent.GetBooleanExtra ("Pop", false);
			var label = FindViewById<TextView> (Resource.Id.label);

			if (pop) {
				label.Text = Intent.GetStringExtra ("SecondData") ?? "Data not available";
			} else {
				Toast.MakeText (this, "Forgot to poke", ToastLength.Short).Show();
				label.Text = "no poke recieved";
			}

			var progress = FindViewById<TextView> (Resource.Id.progress);
			if (pop) {
				progress.Text = count.ToString () + progress.Text;
			} else {
				progress.Text = "poke to count";
			}

			var poker = FindViewById<Button> (Resource.Id.poker);
			poker.Text = poker.Text + " Second Activity";
			poker.Click += (sender, e) => {
				count++;
				pop = true;
				poker.Enabled = false;

			};
			var showSecond = FindViewById<Button> (Resource.Id.switcher);
			showSecond.Click += (sender, e) => 
			{

				var second = new Intent(this, typeof(SecondActivity));
				second.PutExtra("FirstData", "Data Recieved from 1");
				second.PutExtra("Progress", count);
				second.PutExtra("Pop", pop);
				second.SetFlags(ActivityFlags.ClearTop);
				StartActivity(second);
				this.Finish();
				return;
			};
		}
	}
}


