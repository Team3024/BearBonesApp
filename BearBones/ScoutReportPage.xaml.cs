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

		string[] drvType = { "Mecanum", "Tank", "Swerve", "Other" };
		string[] ptsScored = {"0","5","10","20","30","40","50","60","70","80","90","100"};
		string[] bldQuality = {"Superb","High","OK","Low","Poor","Junk"};
		string[] tmQuality = {"Superb","High","OK","Low","Poor"};
		string[] maxStak = {"0","1","2","3","4","5","6"};
		string[] autoCap = {"Never Moved","In AutoZone","1 Can","1 Tote","Tote Stack"};
		string[] lstYear = {"World","District","Prelims"};

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
			model.driveType = drvType[driveType.SelectedIndex];
			model.pointsScored = ptsScored [pointsScored.SelectedIndex];
			model.buildQuality = bldQuality[buildQuality.SelectedIndex];
			model.autoCapability = autoCap[autoCapability.SelectedIndex];
			model.maxStack = maxStak[maxStack.SelectedIndex];
			model.teamQuality = tmQuality[teamQuality.SelectedIndex];

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

