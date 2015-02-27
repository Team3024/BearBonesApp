using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using XLabs.Forms;
using XLabs.Forms.Controls;
using XLabs.Forms.Charting;
using XLabs.Forms.Charting.Controls;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Serialization;
using XLabs.Platform.Services.Media;

using HockeyApp;

[assembly: Xamarin.Forms.Dependency (typeof (IDevice))]
[assembly: Dependency(typeof(BearBones.Android.Picture))]
[assembly: Dependency(typeof(BearBones.Android.AWS))]
[assembly: Dependency(typeof(BearBones.Platform.Services.Media.MediaPicker))]
[assembly: Dependency(typeof(BearBones.Platform.Services.Media.MediaPickerActivity))]
//[assembly: Dependency(typeof(BearBones.Media.IMediaPicker))]

namespace BearBones.Android
{
	using DataNuage.Aws;
	[Activity (Label = "BearBones.Android.Android", MainLauncher = true, ScreenOrientation=ScreenOrientation.Portrait , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity
	{
		public const string HOCKEYAPP_APPID = "com.symbolicflight.bearbones";
		protected override void OnCreate (Bundle bundle)
		{




			SetIoc ();
			base.OnCreate (bundle);
			/*
			// Register the crash manager before Initializing the trace writer
			HockeyApp.CrashManager.Register (this, HOCKEYAPP_APPID); 

			//Register to with the Update Manager
			HockeyApp.UpdateManager.Register (this, HOCKEYAPP_APPID);

			// Initialize the Trace Writer
			HockeyApp.TraceWriter.Initialize ();

			// Wire up Unhandled Expcetion handler from Android
			AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) => 
			{
				// Use the trace writer to log exceptions so HockeyApp finds them
				HockeyApp.TraceWriter.WriteTrace(args.Exception);
				args.Handled = true;
			};

			// Wire up the .NET Unhandled Exception handler
			AppDomain.CurrentDomain.UnhandledException +=
				(sender, args) => HockeyApp.TraceWriter.WriteTrace(args.ExceptionObject);

			// Wire up the unobserved task exception handler
			TaskScheduler.UnobservedTaskException += 
				(sender, args) => HockeyApp.TraceWriter.WriteTrace(args.Exception);
			*/
			Xamarin.Forms.Forms.Init (this, bundle);
			HomePage.ScreenWidth = (int)Resources.DisplayMetrics.WidthPixels; // real pixels
			HomePage.ScreenHeight = (int)Resources.DisplayMetrics.HeightPixels; // real pixels
			SetPage (App.GetMainPage ());
		}

		private void SetIoc()
		{
			//var resolverContainer = new SimpleContainer();
			var resolverContainer = new XLabs.Ioc.SimpleContainer ();
			resolverContainer
				.Register< XLabs.Platform.Device.IDevice> (t => AndroidDevice.CurrentDevice)
				.Register<XLabs.Platform.Device.IDisplay> (t => t.Resolve<XLabs.Platform.Device.IDevice> ().Display)
				.Register<IMediaPicker>(t => new BearBones.Platform.Services.Media.MediaPicker())
				.Register<XLabs.Ioc.IDependencyContainer> (t => resolverContainer);
			XLabs.Ioc.Resolver.SetResolver (resolverContainer.GetResolver ());
		}
	}
}

