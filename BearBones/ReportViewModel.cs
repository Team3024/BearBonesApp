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
		public string teamName { set; get; }
		public int teamNumber {  set; get; }
		public string matchNumber {  set; get; }
		public string notes {  set; get; }

		public string allianceScore {  set; get; }	//0,5,10,20,30,40,50-200
		public int canScore {  set; get; }	//0,5,10,20,30,40,50-200
		public int toteScore {  set; get; }	//0,5,10,20,30,40,50-200
		public int noodleScore {  set; get; }	//0,5,10,20,30,40,50-200
		public int coopScore {  set; get; }	//0,5,10,20,30,40,50-200

		public string autoCapability {  set; get; }	//never moved,in-zone, 1 container, 1 tote, tote stack
		public bool brokeDown {  set; get; }
		public bool swich {  set; get; }
	}
}

