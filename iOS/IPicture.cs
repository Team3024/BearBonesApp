using System;
using Xamarin.Forms;

namespace BearBones.IOS
{
	public interface IPicture
	{

		void SavePictureToDisk (ImageSource imgSrc, Guid Id);

		//string GetPictureFromDisk (int id);
	}
}

