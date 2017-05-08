using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Android.App;

namespace EvolveLite
{
	[Application]
	public class EvolveApp : Application
	{

		// TODO: DemoiOS1: Singleton reference to the database manager class
		public static ConferenceDatabase Database { get { return databaseInstance; } }
		static readonly ConferenceDatabase databaseInstance = new ConferenceDatabase (ConferenceDatabase.DatabaseFilePath);

		public EvolveApp(IntPtr handle, Android.Runtime.JniHandleOwnership transfer)
			: base(handle,transfer)
		{
		}
	}
}

