using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using XLabs.Data;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;


//using DataNuage.Aws;


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
		ReportViewModel model0,model1,model2;
		InfoPage iPage;
		//IMediaPicker mediaPicker;

		string[] drvType = { "Mecanum", "Tank", "Swerve", "Other","none" };
		string[] allianceScor = {"0","5","10","20","30","40","50","60","70","80","90","100","150","200","250","none"};
		string[] toteScor = {"0","2","4","6","8","10","12"};
		string[] canScor = {"0","4","8","12","16","20","24","28","32"};
		string[] bldQuality = {"Superb","High","OK","Low","Poor","Junk","none"};
		string[] tmQuality = {"Superb","High","OK","Low","Poor","none"};
		string[] maxStak = {"0","1","2","3","4","5","6","none"};
		string[] autoCap = {"Never Moved","In AutoZone","1 Can","1 Tote","Tote Stack","none"};
		string[] lstYear = {"World","District","Prelims","none"};

		public ScoutReportPage (InfoPage ip,string number)
		{
			InitializeComponent ();
			iPage = ip;
			model0 = new ReportViewModel ();
			model1 = new ReportViewModel ();
			model2 = new ReportViewModel ();

			model0.teamName = "???";
			model1.teamName = "???";
			model2.teamName = "???";

			int n;

			if (number != null)// use the selected team number string
			{
				int.TryParse (number, out n);
				model0.teamNumber = n;
			}
			else
				model0.teamNumber = 0;
			// default aliance members
			model1.teamNumber = 0;
			model2.teamNumber = 0;
			
			Random rand = new Random ();
			//model.pointsScored = rand.Next().ToString();
			model0.matchNumber = 0.ToString();//rand.Next().ToString();
			model1.matchNumber = 0.ToString();
			model2.matchNumber = 0.ToString();

			for (int i = 0; i < 400; ++i)
			{
				allianceScore.Items.Add (i.ToString());
			}
		
			canScore0.SelectedIndexChanged += (object sender, EventArgs e) => 
			{
				canScore0.Unfocus();
				model0.canScore+=canScore0.SelectedIndex*4;
				canScoreTitle0.Text="Cans:"+model0.canScore.ToString();
				canScore0.SelectedIndex=0;
			};

			toteScore0.SelectedIndexChanged += (object sender, EventArgs e) => 
			{
				toteScore0.Unfocus();
				model0.toteScore+=toteScore0.SelectedIndex*2;
				toteScore0.SelectedIndex = 0;//On the screen picker stay at the old value
				toteScoreTitle0.Text="Totes:"+model0.toteScore.ToString();
				toteScore0.SelectedIndex=0;
			};

			noodleScore0.SelectedIndexChanged += (object sender, EventArgs e) => {
				noodleScore0.Unfocus ();
				model0.noodleScore += noodleScore0.SelectedIndex * 4;
				noodleScoreTitle0.Text = "Noodles:" + model0.noodleScore.ToString ();
				noodleScore0.SelectedIndex = 0;
			};

			coopScore0.SelectedIndexChanged += (object sender, EventArgs e) => {
				coopScore0.Unfocus ();
				model0.coopScore += coopScore0.SelectedIndex * 4;
				coopScoreTitle0.Text = "Coop:" + model0.coopScore.ToString ();
				coopScore0.SelectedIndex = 0;
			};

			canScore1.SelectedIndexChanged += (object sender, EventArgs e) => 
			{
				canScore1.Unfocus();
				model1.canScore+=canScore1.SelectedIndex*4;
				canScoreTitle1.Text="Cans:"+model1.canScore.ToString();
				canScore1.SelectedIndex=0;
			};

			toteScore1.SelectedIndexChanged += (object sender, EventArgs e) => 
			{
				toteScore1.Unfocus();
				model1.toteScore+=toteScore1.SelectedIndex*2;
				toteScore1.SelectedIndex = 0;//On the screen picker stay at the old value
				toteScoreTitle1.Text="Totes:"+model1.toteScore.ToString();
				toteScore1.SelectedIndex=0;
			};

			noodleScore1.SelectedIndexChanged += (object sender, EventArgs e) => {
				noodleScore1.Unfocus ();
				model1.noodleScore += noodleScore1.SelectedIndex * 4;
				noodleScoreTitle1.Text = "Noodles:" + model1.noodleScore.ToString ();
				noodleScore1.SelectedIndex = 0;
			};

			coopScore1.SelectedIndexChanged += (object sender, EventArgs e) => {
				coopScore1.Unfocus ();
				model1.coopScore += coopScore1.SelectedIndex * 4;
				coopScoreTitle1.Text = "Coop:" + model1.coopScore.ToString ();
				coopScore1.SelectedIndex = 0;
			};

			canScore2.SelectedIndexChanged += (object sender, EventArgs e) => 
			{
				canScore2.Unfocus();
				model2.canScore+=canScore2.SelectedIndex*4;
				canScoreTitle2.Text="Cans:"+model2.canScore.ToString();
				canScore2.SelectedIndex=0;
			};

			toteScore2.SelectedIndexChanged += (object sender, EventArgs e) => 
			{
				toteScore2.Unfocus();
				model2.toteScore+=toteScore2.SelectedIndex*2;
				toteScore2.SelectedIndex = 0;//On the screen picker stay at the old value
				toteScoreTitle2.Text="Totes:"+model2.toteScore.ToString();
				toteScore2.SelectedIndex=0;
			};

			noodleScore2.SelectedIndexChanged += (object sender, EventArgs e) => {
				noodleScore2.Unfocus ();
				model2.noodleScore += noodleScore2.SelectedIndex * 4;
				noodleScoreTitle2.Text = "Noodles:" + model2.noodleScore.ToString ();
				noodleScore2.SelectedIndex = 0;
			};

			coopScore2.SelectedIndexChanged += (object sender, EventArgs e) => {
				coopScore2.Unfocus ();
				model2.coopScore += coopScore2.SelectedIndex * 4;
				coopScoreTitle2.Text = "Coop:" + model2.coopScore.ToString ();
				coopScore2.SelectedIndex = 0;
			};
			//model.scoutName = "JOHNNY FIVE";


		}


		private void harvestUIControls()
		{

			if (allianceScore.SelectedIndex >= 0) {
				model0.allianceScore = allianceScor [allianceScore.SelectedIndex];
				model1.allianceScore = allianceScor [allianceScore.SelectedIndex];
			} else {
				model0.allianceScore = allianceScor [allianceScor.Length - 1];				
				model1.allianceScore = allianceScor [allianceScor.Length - 1];
			}

			if (autoCapability0.SelectedIndex >= 0) {
				model0.autoCapability = autoCap [autoCapability0.SelectedIndex];
			} else {
				model0.autoCapability = autoCap [autoCap.Length - 1];
			}

			model0.brokeDown = brokeDown0.IsToggled;
			model0.notes = notes.Text;
			model0.matchNumber = match.Text;

			model1.brokeDown = brokeDown1.IsToggled;
			model1.notes = notes.Text;
			model1.matchNumber = match.Text;
		}
			

		void OnCancelClicked (object sender, EventArgs e)
		{
			model0.canScore = 0;
			model0.toteScore = 0;

			// leave this page
			Navigation.PopModalAsync ();
		}

		void OnDoneClicked (object sender, EventArgs e)
		{
			harvestUIControls();
			Rest rest = new Rest ();
			rest.createNewReport (model0);
			rest.createNewReport (model1);
			rest.createNewReport (model2);
			//iPage.Refresh ();
			// leave this page
			Navigation.PopModalAsync ();
		}
	}
}

