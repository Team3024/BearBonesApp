using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using XLabs.Forms.Charting;
using XLabs.Forms.Charting.Controls;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Forms.Controls;
using XLabs.Serialization;
using XLabs.Platform.Services.Media;
namespace BearBones.Android
{
	[Activity (Label = "BearBones.Android.Android", MainLauncher = true, ScreenOrientation=ScreenOrientation.Portrait , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetIoc ();
			Xamarin.Forms.Forms.Init (this, bundle);
			HomePage.ScreenWidth = (int)Resources.DisplayMetrics.WidthPixels; // real pixels
			HomePage.ScreenHeight = (int)Resources.DisplayMetrics.HeightPixels; // real pixels
			SetPage (App.GetMainPage ());

		}

		private void SetIoc()
		{
			//var resolverContainer = new SimpleContainer();
			var resolverContainer = new XLabs.Ioc.SimpleContainer ();
			resolverContainer.Register< XLabs.Platform.Device.IDevice> (t => AndroidDevice.CurrentDevice)
				.Register<XLabs.Platform.Device.IDisplay> (t => t.Resolve<XLabs.Platform.Device.IDevice> ().Display)
				.Register<IMediaPicker>(t => new MediaPicker())
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

