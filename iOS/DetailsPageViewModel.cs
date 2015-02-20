using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace BearBones.iOS
{
	public class DetailsPageViewModel
	{


		private ObservableCollection<Item> items;


		public ObservableCollection<Item> Items {

			get { return items; }

			set {

				items = value;

			}

		}



		private Item currentItem;


		public Item CurrentItem {

			get { return currentItem; }

			set {

				currentItem = value;

			}

		}


		private ImageSource imageSource;


		public ImageSource ImageSource {

			get {

				return imageSource;

			}

			set {

				imageSource = value;

			}

		}




		public void GetImage (int Id)

		{

			string fileName = DependencyService.Get<IPicture> ()

				.GetPictureFromDisk (Id);

			ImageSource = ImageSource.FromFile (fileName);

		}

	}
}

