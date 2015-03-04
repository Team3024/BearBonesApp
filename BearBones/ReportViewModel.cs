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
		public double timestamp {  set; get; }

		public string reportType {  set; get; }

		public string teamName { set; get; }

		public int teamNumber {  set; get; }

		public string scoutName {  set; get; }

		public string matchNumber {  set; get; }

		public string driveType {  set; get; }		//swerve,mecanum,tank,other
		public string buildQuality {  set; get; }	//junk,low,ok,high,superb
		public string lastYearFinish {  set; get; }	//prelim,regional,world
		public string autoCapability {  set; get; }	//never moved,in-zone, 1 container, 1 tote, tote stack
		public string maxStack {  set; get; }		//1,2,3,4,5,6
		public bool brokeDown {  set; get; }

		public bool noodleInContainer {  set; get; }
		public bool noodleCleanup {  set; get; }
		public bool noodleBonus {  set; get; }


		public bool grabsContainer {  set; get; }
		public bool grabsContainerOffStep {  set; get; }

		public bool setsContainerOnStack {  set; get; }
		public bool yellowCoopStack {  set; get; }
		public bool stacksTotes {  set; get; }
		public bool rebuildsStack {  set; get; }

		public bool grabsTote {  set; get; }
		public bool grabsToteOffStep {  set; get; }


		public string teamQuality {  set; get; }	//bad,poor,ok,good,superb
		public string notes {  set; get; }
		//add photo
		public string allianceScore {  set; get; }	//0,5,10,20,30,40,50-200
		public int canScore {  set; get; }	//0,5,10,20,30,40,50-200
		public int toteScore {  set; get; }	//0,5,10,20,30,40,50-200

	}
}

