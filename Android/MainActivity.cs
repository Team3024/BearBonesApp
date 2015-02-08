using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;


namespace BearBones.Android
{
	[Activity (Label = "BearBones.Android.Android", MainLauncher = true, ScreenOrientation=ScreenOrientation.Portrait , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);
			HomePage.ScreenWidth = (int)Resources.DisplayMetrics.WidthPixels; // real pixels
			HomePage.ScreenHeight = (int)Resources.DisplayMetrics.HeightPixels; // real pixels
			SetPage (App.GetMainPage ());

		}
	}
}

