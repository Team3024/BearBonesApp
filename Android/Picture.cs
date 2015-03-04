using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS.Storage;
using Android.Graphics;
using Android.Webkit;


namespace BearBones.Android
{
	public class Picture : IPicture
	{
		public async void SavePictureToDisk (ImageSource imgSrc, string name) //public void Save(Stream stream, string _name)
		{

			var renderer = new StreamImagesourceHandler ();

			var bitmap = await renderer.LoadImageAsync (imgSrc,null,new System.Threading.CancellationToken());

			//if (name.ToLower ().Contains (".jpg") || name.ToLower ().Contains (".png")) {
				//stream.Position = 0;
			//var bitmap = BitmapFactory.DecodeStream ((System.IO.Stream)stream);

				var finalStream = new MemoryStream ();

				bitmap.Compress (Bitmap.CompressFormat.Jpeg, 25, finalStream);
				bitmap = null;

				finalStream.Position = 0;

				var path2 = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var filename2 = System.IO.Path.Combine (path2, name+".jpg");

				using (var fileStream = File.Create (filename2))
				{
					finalStream.Seek (0, SeekOrigin.Begin);
					finalStream.CopyTo (fileStream);
					fileStream.Close ();

					finalStream.Dispose ();
					//stream.Dispose ();
					fileStream.Dispose ();
					GC.Collect ();
				}
				return;
			//}

			//stream.Position = 0;

			var path = Environment.GetFolderPath (Environment.SpecialFolder.MyPictures);
			var filename = System.IO.Path.Combine (path, name+".jpg");
		
			using (var fileStream = File.Create (filename))
			{
				//stream.Seek (0, SeekOrigin.Begin);
				//stream.CopyTo (fileStream);
				fileStream.Close ();

				//stream.Dispose ();
				fileStream.Dispose ();
				GC.Collect ();
			}

		}

		public string GetPictureFromDisk(string name)
		{

			var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//Environment.GetFolderPath(name);
			string jpgFilename = System.IO.Path.Combine (documentsDirectory, name+".jpg");
			if (File.Exists (jpgFilename))
				return jpgFilename;
			else
				return null;
		}
	}
}


