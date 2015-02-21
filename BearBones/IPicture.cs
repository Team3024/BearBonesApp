
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BearBones
{
	public interface IPicture
	{

		void SavePictureToDisk (ImageSource imgSrc, Guid Id);

		string GetPictureFromDisk (string name);
	}
}


