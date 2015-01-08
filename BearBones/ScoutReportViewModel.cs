using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BearBones
{
	class ScoutReportViewModel
	{
		public ScoutReportViewModel(string tNumber)
		{
			this.teamNumber = tNumber;
		}

		internal string teamName { set; get; }

		internal string teamNumber {  set; get; }

		internal string scoutName {  set; get; }

		internal string matchNumber {  set; get; }
	}
}

