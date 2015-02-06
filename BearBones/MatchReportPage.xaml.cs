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
	public partial class MatchReportPage : ContentPage
	{	
		ReportViewModel model; 

		public MatchReportPage (string number)
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
			model.pointsScored = rand.Next().ToString();
			model.driveType = "mecanum";
			model.matchNumber = rand.Next();
			model.reportType = "match";
			model.scoutName = "Yoda";

			Rest rest = new Rest ();
			rest.createNewReport (model);

		}
		void OnDoneClicked (object sender, EventArgs e)
		{
			// leave this page
			Navigation.PopModalAsync ();
		}
	}
}

