using System;
using System.Collections.Generic;
using Xamarin.Forms;
/*
SCOUT REPORT

drive picker--swerve,mecanum,tank,other
build quality--junk,low,ok,high,superb
last year--prelim,regional,world
noodle in cont: yes/no
Cont on stack yes/no
noodle cleanup
noodle bonus
add photo

MATCH REPORT

Breakdown: yes/no
noodle in cont: yes/no
Cont on stack yes/no
Points scored: 0,5,10,20,30,40,50-200
coop stack: yes/no
auto: never moved,in-zone, 1 container, 1 tote, tote stack
grab containers off step: yes/no
grab totes off step: Yes/no
rebuild stacks.
noodle cleanup
noodle bonus
Team Rating: bad,poor,ok,good,superb
Notes:?

Save Report




*/
namespace BearBones
{	
	public partial class ScoutReportPage : ContentPage
	{	
		ReportViewModel model;

		string[] drvType = { "Mecanum", "Tank", "Swerve", "Other","none" };
		string[] allianceScor = {"0","5","10","20","30","40","50","60","70","80","90","100","150","200","250","none"};
		string[] toteScor = {"2","4","6","8","10","12","none"};
		string[] canScor = {"4","8","12","16","20","24","28","32","none"};
		string[] bldQuality = {"Superb","High","OK","Low","Poor","Junk","none"};
		string[] tmQuality = {"Superb","High","OK","Low","Poor","none"};
		string[] maxStak = {"0","1","2","3","4","5","6","none"};
		string[] autoCap = {"Never Moved","In AutoZone","1 Can","1 Tote","Tote Stack","none"};
		string[] lstYear = {"World","District","Prelims","none"};

		public ScoutReportPage (string number)
		{
			InitializeComponent ();
			model = new ReportViewModel ();
			model.teamName = "???";
			int n;
			if (number != null) {
				int.TryParse (number, out n);
				model.teamNumber = n;
			}
			else
				model.teamNumber = 0;
			Random rand = new Random ();
			//model.pointsScored = rand.Next().ToString();
			model.matchNumber = rand.Next().ToString();
			model.reportType = "scout";
			//model.scoutName = "JOHNNY FIVE";


		}

		private void harvestUIControls()
		{
			if(driveType.SelectedIndex>=0)
				model.driveType = drvType[driveType.SelectedIndex];
			else
				model.driveType = drvType[drvType.Length-1];

			if(allianceScore.SelectedIndex>=0)
				model.allianceScore = allianceScor [allianceScore.SelectedIndex];
			else
				model.allianceScore = allianceScor [allianceScor.Length-1];

			if(buildQuality.SelectedIndex>=0)
				model.buildQuality = bldQuality[buildQuality.SelectedIndex];
			else
				model.buildQuality = bldQuality[bldQuality.Length-1];

			if(autoCapability.SelectedIndex>=0)
				model.autoCapability = autoCap[autoCapability.SelectedIndex];
			else
				model.autoCapability = autoCap[autoCap.Length-1];

			if(maxStack.SelectedIndex>=0)
				model.maxStack = maxStak[maxStack.SelectedIndex];
			else
				model.maxStack = maxStak[maxStak.Length-1];

			if(teamQuality.SelectedIndex>=0)
				model.teamQuality = tmQuality[teamQuality.SelectedIndex];
			else
				model.teamQuality = tmQuality[tmQuality.Length-1];

			if(lastYear.SelectedIndex>=0)
				model.lastYearFinish = lstYear[lastYear.SelectedIndex];
			else
				model.lastYearFinish = lstYear[lstYear.Length-1];

			model.noodleInContainer = noodleInContainer.IsToggled;
			model.noodleCleanup = noodleCleanup.IsToggled;
			model.noodleBonus = noodleBonus.IsToggled;

			model.brokeDown = brokeDown.IsToggled;
			model.rebuildsStack = rebuildsStack.IsToggled;
			model.yellowCoopStack = yellowCoopStack.IsToggled;

			model.setsContainerOnStack = containerOnStack.IsToggled;
			model.grabsContainer = grabsContainer.IsToggled;
			model.grabsContainerOffStep = grabsContainerOffStep.IsToggled;

			model.stacksTotes = stacksTotes.IsToggled;
			model.grabsTote = grabsTote.IsToggled;
			model.grabsToteOffStep = grabsToteOffStep.IsToggled;
			model.notes = notes.Text;
			model.scoutName = scout.Text;
			model.matchNumber = match.Text;

		}

		void OnCancelClicked (object sender, EventArgs e)
		{
			// leave this page
			Navigation.PopModalAsync ();
		}

		void OnDoneClicked (object sender, EventArgs e)
		{
			harvestUIControls();
			Rest rest = new Rest ();
			rest.createNewReport (model);
			// leave this page
			Navigation.PopModalAsync ();
		}
	}
}

