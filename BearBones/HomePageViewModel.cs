using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BearBones
{
	public class HomePageViewModel
	{
		public HomePageViewModel(Type pageType)
		{
			this.PageType = pageType;
			this.PageName = teamNumber + " : " + teamName;
		}

		public Type PageType { private set; get; }

		public string PageName { set; get; }

		public string teamName { set; get; }

		public int teamNumber {  set; get; }

		public string scoutName {  set; get; }

		public int index {  set; get; }

	}
}

