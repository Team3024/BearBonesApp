using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BearBones
{
	class HomePageViewModel
	{
		public HomePageViewModel(Type pageType,
			Action<String> infoExecute, 
			Action<String> scoutReportExecute,
			Action<String> matchReportExecute)
		{
			this.PageType = pageType;
			this.PageName = teamName + " : " + teamNumber;
			this.InfoCommand = new Command<String>(infoExecute);
			this.ScoutReportCommand = new Command<String>(scoutReportExecute);
			this.MatchReportCommand = new Command<String>(matchReportExecute);

		}

		public Type PageType { private set; get; }

		public string PageName { set; get; }

		public ICommand InfoCommand { private set; get; }

		public ICommand ScoutReportCommand { private set; get; }

		public ICommand MatchReportCommand { private set; get; }

		public string teamName { set; get; }

		public string teamNumber {  set; get; }

		public string scoutName {  set; get; }
	}
}

