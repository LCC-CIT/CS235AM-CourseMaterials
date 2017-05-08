//
// Main.cs: creates a new Xamarin.iOS application instance and passes the name of the class that will handle OS events, AppDelegate.
//
// Author:
//   George Seiler (george.seiler@xamarin.com)
//
// Copyright (C) 2013 Xamarin, Inc (http://www.xamarin.com)
//
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Phoneword_iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
