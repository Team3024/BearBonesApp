using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BearBones
{
	public class ReportViewModel
	{
		public ReportViewModel()
		{
			/*
			if (tNumber != null)
			{
				int n;
				int.TryParse(tNumber,out n);
				this.teamNumber = n;
			}
			*/
		}

		public string reportType {  set; get; }

		public string teamName { set; get; }

		public int teamNumber {  set; get; }

		public string scoutName {  set; get; }

		public int matchNumber {  set; get; }

		public string driveType {  set; get; }

		public int score {  set; get; }
	}
}

