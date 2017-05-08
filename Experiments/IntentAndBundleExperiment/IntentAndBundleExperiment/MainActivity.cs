using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace IntentAndBundleExperiment
{
	[Activity (Label = "Intents and Bundles", MainLauncher = true)]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.ageButton);
			EditText etName = FindViewById<EditText> (Resource.Id.nameEditText);
			EditText etBirthday = FindViewById<EditText> (Resource.Id.birthdayEditText);

			button.Click += delegate {
				if( "" != etName.Text && "" != etBirthday.Text)
				{
					Intent inBirthday = new Intent(this, typeof(DataActivity));
					inBirthday.PutExtra("Name", etName.Text);
					DateTime dtBirthday = Convert.ToDateTime(etBirthday.Text);
					inBirthday.PutExtra("Birthday", etBirthday.Text);
					int age = DateTime.Now.Subtract( dtBirthday).Days / 365;
					inBirthday.PutExtra("Age", age);
					StartActivity(inBirthday);
				}
				else
				{
					Toast.MakeText(this, "Please enter your name and birth date", ToastLength.Short).Show();
				}
			};
		}
	}
}


