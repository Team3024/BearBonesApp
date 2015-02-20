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
	public class Picture
	{
		public async void SavePictureToDisk (ImageSource imgSrc, Guid Id) //public void Save(Stream stream, string _name)
		{
			var _filename = Id.ToString ();//_name;

			var renderer = new StreamImagesourceHandler ();

			var bitmap = await renderer.LoadImageAsync (imgSrc,null,new System.Threading.CancellationToken());

			//if (_filename.ToLower ().Contains (".jpg") || _filename.ToLower ().Contains (".png")) {
				//stream.Position = 0;
			//var bitmap = BitmapFactory.DecodeStream ((System.IO.Stream)stream);

				var finalStream = new MemoryStream ();

				bitmap.Compress (Bitmap.CompressFormat.Jpeg, 25, finalStream);
				bitmap = null;

				finalStream.Position = 0;

				var path2 = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var filename2 = System.IO.Path.Combine (path2, _filename);

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

			var path = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var filename = System.IO.Path.Combine (path, _filename);
		
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
	}
}


