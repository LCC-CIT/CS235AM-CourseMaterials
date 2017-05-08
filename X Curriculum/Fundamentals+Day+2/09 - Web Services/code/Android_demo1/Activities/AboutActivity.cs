using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Text.Method;
using Android.Webkit;

namespace EvolveLite
{
	[Activity (Label = "About")]
	public class AboutActivity : Activity
	{

		TextView json, xml;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.about);

			xml = FindViewById<TextView>(Resource.Id.textView1);
			xml.MovementMethod = new ScrollingMovementMethod();
			json = FindViewById<TextView>(Resource.Id.textView2);
			json.MovementMethod = new ScrollingMovementMethod();
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			// XML
			if (System.IO.File.Exists (SessionsXmlParser.Instance.SessionsXmlFilePath))
				xml.Text = System.IO.File.ReadAllText (SessionsXmlParser.Instance.SessionsXmlFilePath);
			// JSON
			if (System.IO.File.Exists (SpeakersJsonParser.Instance.SpeakersJsonFilePath))
				json.Text = System.IO.File.ReadAllText (SpeakersJsonParser.Instance.SpeakersJsonFilePath);
		}
	}
}