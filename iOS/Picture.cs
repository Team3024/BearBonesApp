using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Foundation;

namespace BearBones.IOS
{
	public class Picture : IPicture
	{
		public async void SavePictureToDisk (ImageSource imgSrc, Guid Id)

		{

			var renderer = new StreamImagesourceHandler ();

			var photo = await renderer.LoadImageAsync (imgSrc);

			var documentsDirectory = Environment.GetFolderPath

				(Environment.SpecialFolder.Personal);

			string jpgFilename = System.IO.Path.Combine (

				documentsDirectory, Id.ToString () + ".jpg");

			NSData imgData = photo.AsJPEG ();

			NSError err = null;

			if (imgData.Save (jpgFilename, false, out err)) {

				Console.WriteLine ("saved as " + jpgFilename);

			} else {

				Console.WriteLine ("NOT saved as " + jpgFilename +

					" because...");

			}


		}

		public string GetPictureFromDisk(string name)
		{

			var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string jpgFilename = System.IO.Path.Combine (documentsDirectory, name+".jpg");
			if (File.Exists (jpgFilename))
				return jpgFilename;
			else
				return null;
		}

	}
}