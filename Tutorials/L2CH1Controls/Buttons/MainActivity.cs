using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Buttons
{
	[Activity (Label = "Buttons", MainLauncher = true)]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			TextView output = FindViewById<TextView> (Resource.Id.myTextView);

			Button button1 = FindViewById<Button> (Resource.Id.leftButton);
			button1.Click += delegate {
				output.Text = "Sting!";
			};

			Button button2 = FindViewById<Button> (Resource.Id.rightButton);
			button2.Click += delegate {
				output.Text = "Buzz Buzz!";
			};

			EditText input = FindViewById<EditText> (Resource.Id.myEditText);
			Button button3 = FindViewById<Button> (Resource.Id.myButton);
			button3.Click += delegate {
				output.Text = input.Text;
			};
		}
	}
}


