
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BearBones
{
	public interface IPicture
	{

		void SavePictureToDisk (ImageSource imgSrc, string name);

		string GetPictureFromDisk (string name);
	}
}


