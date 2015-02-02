using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BearBones
{
	public class ReportViewModel
	{
		public ReportViewModel(string tNumber)
		{
			if (tNumber != null)
			{
				int n;
				int.TryParse(tNumber,out n);
				this.teamNumber = n;
			}
		}

		internal string reportType {  set; get; }

		internal string teamName { set; get; }

		internal int teamNumber {  set; get; }

		internal string scoutName {  set; get; }

		internal int matchNumber {  set; get; }
	}
}

