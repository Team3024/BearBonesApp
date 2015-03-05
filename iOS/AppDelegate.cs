

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Foundation;
using UIKit;

using Xamarin.Forms;
using XLabs.Forms.Charting;
using XLabs.Forms.Charting.Controls;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Forms;
using XLabs.Forms.Controls;
using XLabs.Serialization;
using XLabs.Serialization.JsonNET;

using HockeyApp;

[assembly: Xamarin.Forms.Dependency (typeof (IDevice))]
[assembly: Dependency(typeof(BearBones.IOS.Picture))]
//[assembly: Dependency(typeof(BearBones.iOS.AWS))]

namespace BearBones.IOS
{
	using DataNuage.Aws;

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
			//We MUST wrap our setup in this block to wire up
			// Mono's SIGSEGV and SIGBUS signals
			HockeyApp.Setup.EnableCustomCrashReporting (() => {

				//Get the shared instance
				var manager = BITHockeyManager.SharedHockeyManager;

				//Configure it to use our APP_ID
				manager.Configure ("com.symbolicflight.bearbones");

				//Start the manager
				manager.StartManager ();

				//Authenticate (there are other authentication options)
				//manager.Authenticator.AuthenticateInstallation ();

				//Rethrow any unhandled .NET exceptions as native iOS 
				// exceptions so the stack traces appear nicely in HockeyApp
				AppDomain.CurrentDomain.UnhandledException += (sender, e) => 
					Setup.ThrowExceptionAsNative(e.ExceptionObject);

				TaskScheduler.UnobservedTaskException += (sender, e) => 
					Setup.ThrowExceptionAsNative(e.Exception);
			});
			SetIoc ();
			Forms.Init ();
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.MakeKeyAndVisible ();
			HomePage.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;
			HomePage.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
			window.RootViewController = App.GetMainPage ().CreateViewController ();
			new ChartRenderer();
			return true;
		}



		private void SetIoc()
		{
			//var resolverContainer = new SimpleContainer();
			var resolverContainer = new XLabs.Ioc.SimpleContainer ();
			resolverContainer.Register< XLabs.Platform.Device.IDevice> (t => AppleDevice.CurrentDevice)
				.Register<XLabs.Platform.Device.IDisplay> (t => t.Resolve<XLabs.Platform.Device.IDevice> ().Display)
				.Register<XLabs.Ioc.IDependencyContainer> (t => resolverContainer);
			XLabs.Ioc.Resolver.SetResolver (resolverContainer.GetResolver ());

			/*
			resolverContainer.Register<IDevice> (t => AppleDevice.CurrentDevice)
				.Register<IDisplay> (t => t.Resolve<IDevice> ().Display)
				.Register<IDependencyContainer> (t => resolverContainer);
				*/
			/*
			resolverContainer
				.Register<IJsonSerializer, XLabs.Serialization.JsonNET.JsonSerializer>()
				.Register<IDependencyContainer>(t => resolverContainer);
				*/
			//Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}

