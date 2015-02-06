using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Xamarin.Forms;
using XLabs.Forms.Charting;
using XLabs.Forms.Charting.Controls;

namespace BearBones.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public static Page GetMainPage ()
		{   
			return new NavigationPage(new HomePage ());
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.MakeKeyAndVisible ();			
			window.RootViewController = App.GetMainPage ().CreateViewController ();
			HomePage.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;
			HomePage.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
			new ChartRenderer();
			return true;
		}
	}
}

