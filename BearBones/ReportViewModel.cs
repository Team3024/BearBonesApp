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

		public string driveType {  set; get; }		//swerve,mecanum,tank,other
		public string buildQuality {  set; get; }	//junk,low,ok,high,superb
		public string lastYearFinish {  set; get; }	//prelim,regional,world
		public string autoCapability {  set; get; }	//never moved,in-zone, 1 container, 1 tote, tote stack
		public string maxStack {  set; get; }		//1,2,3,4,5,6
		public bool breakdown {  set; get; }

		public bool noodleInContainer {  set; get; }
		public bool noodleCleanup {  set; get; }
		public bool noodleBonus {  set; get; }

		public bool containerOnStack {  set; get; }
		public bool coopStack {  set; get; }
		public bool grabContainer {  set; get; }
		public bool grabContainerFromStep {  set; get; }

		public bool grabTotes {  set; get; }
		public bool grabTotesOffStep {  set; get; }
		public bool rebuildStack {  set; get; }
		public string teamRating {  set; get; }	//bad,poor,ok,good,superb
		public string notes {  set; get; }
		//add photo
		public int pointsScored {  set; get; }	//0,5,10,20,30,40,50-200
	}
}

