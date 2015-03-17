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

		public string score {  set; get; }
		public int toteScore {  set; get; }
		public int canScore {  set; get; }
		public int coopScore {  set; get; }
		public int noodleScore {  set; get; }

		public int reports {  set; get; }

		public string reliability {  set; get; }

		public string auto {  set; get; }
		public string video {  set; get; }

		public int index {  set; get; }

	}
}

