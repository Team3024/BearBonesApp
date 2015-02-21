using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;

namespace BearBones
{
	public interface IAws
	{
		void awsSaveFile (MediaFile img,string name,Label lbl);
		void awsDeleteFile (string name,Label lbl);
		Task<ImageSource> awsGetFile (string name,Label lbl);

	}
}

