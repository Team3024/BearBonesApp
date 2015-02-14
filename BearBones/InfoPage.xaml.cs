using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XLabs.Forms.Charting;
using XLabs.Forms.Charting.Controls;

namespace BearBones
{	



	public partial class InfoPage : CarouselPage
	{	

		public List<string> allianceScore = new List<string> ();
		public List<string> driveTypes = new List<string> ();
		public List<string> scoutNames = new List<string> ();
		public List<string> autoCapabilities = new List<string> ();
		public List<bool> brokeDowns = new List<bool> ();
		public List<string> buildQualities = new List<string> ();
		public List<bool> grabsContainers = new List<bool> ();
		public List<bool> grabsContainerOffSteps = new List<bool> ();
		public List<bool> grabsTotes = new List<bool> ();
		public List<bool> grabsToteOffSteps = new List<bool> ();
		public List<string> lastYearFinishes = new List<string> ();
		public List<string> matchNumbers = new List<string> ();
		public List<string> maxStacks = new List<string> ();
		public List<bool> noodleBonuses = new List<bool> ();
		public List<bool> noodleCleanups = new List<bool> ();
		public List<bool> noodleInContainers = new List<bool> ();
		public List<string> noteses = new List<string> ();
		public List<bool> rebuildsStacks = new List<bool> ();
		public List<string> reportTypes = new List<string> ();
		public List<bool> setsContainerOnStacks = new List<bool> ();
		public List<bool> stacksToteses = new List<bool> ();
		public List<string> teamNames = new List<string> ();
		public List<int> teamNumbers = new List<int> ();
		public List<string> teamQualities = new List<string> ();
		public List<bool> yellowCoopStacks = new List<bool> ();

		public int teamNumber;
		public string teamName;
		public HomePageViewModel hp;

		public InfoPage (HomePageViewModel hpvm)
		{
			if(hpvm != null)
				hp = hpvm;
			InitializeComponent ();
			getReports(hpvm.teamNumber);

			teamNumber = hpvm.teamNumber;
			teamName = hpvm.teamName;

			//title1.Text = hpvm.teamName + " " + hpvm.teamNumber + ": Rprt 1";



		}

		void OnDoneClicked (object sender, EventArgs e)
		{
			// leave this page
			Navigation.PopModalAsync ();

		}

		//test for replacing value of Label
		void ChangeValue(object sender, EventArgs e)
		{

			ObservableCollection<InfoPageViewModel> lv = new ObservableCollection<InfoPageViewModel> ();

			Rest rest = new Rest();

			//Task <ObservableCollection<InfoPageViewModel>> list = rest.SendAndReceiveJsonRequest ("http://71.92.131.203/db/data/cypher/","MATCH (a:Team) RETURN a LIMIT 25");
			/*
			try {
				field1Entry.Text = int.Parse (field1Entry.Text) > 50 ? "impressive" : "could\'ve done better";
			} catch {
				field1Entry.Text = "I\'m sorry. Something didn\'t work.";
			}
			*/
		}

		async void getReports(int tNum)
		{

			Rest rest = new Rest ();
			Task <ObservableCollection<ReportViewModel>> list =  rest.getReports (tNum);//SendAndReceiveJsonRequest ();
			var reports = await list;
			var count = 0;


			foreach (ReportViewModel ip in reports) {
				var p = ip;

				allianceScore.Add (ip.allianceScore);
				driveTypes.Add (ip.driveType);
				scoutNames.Add (ip.scoutName);
				autoCapabilities.Add (ip.autoCapability);
				brokeDowns.Add (ip.brokeDown);
				buildQualities.Add (ip.buildQuality);
				grabsContainers.Add (ip.grabsContainer);
				grabsContainerOffSteps.Add (ip.grabsContainerOffStep);
				grabsTotes.Add (ip.grabsTote);
				grabsToteOffSteps.Add (ip.grabsToteOffStep);
				lastYearFinishes.Add (ip.lastYearFinish);
				matchNumbers.Add (ip.matchNumber);
				maxStacks.Add (ip.maxStack);
				noodleBonuses.Add (ip.noodleBonus);
				noodleCleanups.Add (ip.noodleCleanup);
				noodleInContainers.Add (ip.noodleInContainer);
				noteses.Add (ip.notes);
				rebuildsStacks.Add (ip.rebuildsStack);
				reportTypes.Add (ip.reportType);
				setsContainerOnStacks.Add (ip.setsContainerOnStack);
				stacksToteses.Add (ip.stacksTotes);
				teamNames.Add (ip.teamName);
				teamNumbers.Add (ip.teamNumber);
				teamQualities.Add (ip.teamQuality);
				yellowCoopStacks.Add (ip.yellowCoopStack);
				if (hp != null)
				{
					if (ip.allianceScore != null)
						hp.score = ip.allianceScore.ToString ();
					else
						hp.score = "";

					if (ip.brokeDown != null)
						hp.reliability = ip.brokeDown.ToString ();
					else
						hp.reliability = "";

					if (ip.autoCapability != null)
						hp.auto = ip.autoCapability.ToString ();
					else
						hp.auto = "";
				}

				InfoCell report = new InfoCell ();

				this.Children.Add (report.CreatePage (ip.allianceScore,
					ip.driveType,
					ip.scoutName,
					ip.autoCapability,
					ip.brokeDown,
					ip.buildQuality,
					ip.grabsContainer,
					ip.grabsContainerOffStep,
					ip.grabsTote,
					ip.grabsToteOffStep,
					ip.lastYearFinish,
					ip.matchNumber,
					ip.maxStack,
					ip.noodleBonus,
					ip.noodleCleanup,
					ip.noodleInContainer,
					ip.notes,
					ip.rebuildsStack,
					ip.reportType,
					ip.setsContainerOnStack,
					ip.stacksTotes,
					ip.teamName,
					ip.teamNumber,
					ip.teamQuality,
					ip.yellowCoopStack,
					count));

				//this.Children.Add (report.CreatePage(ip.pointsScored, ip.driveType, ip.scoutName));



				//scores [count] = Convert.ToString (ip.score);
				//drives [count] = Convert.ToString (ip.drive);
				//scouts [count] = Convert.ToString (ip.scout);
				//scores.Add (Convert.ToString(ip.score));

				count++;

			}

			buildAttributes ();
			//Chart chart = await BuildGraphs ();
			//graphs.Children.Add (chart);
			await BuildGraphs ();
	
		}

		async void NewMatchReport(object sender, EventArgs e)
		{
			MatchReportPage page = new MatchReportPage (teamNumber.ToString());
			await Navigation.PushModalAsync (page);
		}

		async void NewScoutReport(object sender, EventArgs e)
		{
			ScoutReportPage page = new ScoutReportPage (teamNumber.ToString());
			await Navigation.PushModalAsync (page);
		}

		void buildAttributes(){

			attributes.Children.Add(BuildSummary ("Grabs Can", grabsContainers[0]));
			attributes.Children.Add(BuildSummary ("Grabs Can of Step", grabsContainerOffSteps[0]));
			attributes.Children.Add(BuildSummary ("Grabs Totes", grabsTotes[0]));
			attributes.Children.Add(BuildSummary ("Grabs Totes off Step", grabsToteOffSteps[0]));
			attributes.Children.Add(BuildSummary ("Noodle Bonus", noodleBonuses[0]));
			attributes.Children.Add(BuildSummary ("Noodle in Can", noodleInContainers[0]));
			attributes.Children.Add(BuildSummary ("Noodle Cleanup", noodleCleanups[0]));
			attributes.Children.Add(BuildSummary ("Rebuild Stacks", rebuildsStacks[0]));
			attributes.Children.Add(BuildSummary ("Can on Stack", setsContainerOnStacks[0]));
			attributes.Children.Add(BuildSummary ("Stack Totes", stacksToteses[0]));
			attributes.Children.Add(BuildSummary ("Co-op Stack", yellowCoopStacks[0]));
		}


		Label BuildSummary(string labelText, bool attribute)
		{
			var indicatorColor = Color.White;
			if (attribute) {
				indicatorColor = Color.Green;
			} else {
				indicatorColor = Color.Red;
			}
			Label LabelText = new Label{ Text = labelText, BackgroundColor = indicatorColor };
			return LabelText;
		}

		async Task BuildGraphs()
		{
			if (allianceScore.Count > 0) {

				Series scoreSeries = new Series {
					Color = Color.Red,
					Type = ChartType.Line,
				};
					

				Series breakDownSeries = new Series {
					Color = Color.Blue,
					Type = ChartType.Line
				};

				Series autoSeries = new Series {
					Color = Color.White,
					Type = ChartType.Line
				};

				foreach (var score in allianceScore) {
					int scoreInt;

					if (score == null) {
						scoreInt = -1;
					} else {
						int.TryParse (score, out scoreInt);
					}

					scoreSeries.Points.Add (new DataPoint (){ Label = score, Value = scoreInt });
				}
				int counter = 1;
				foreach (var brokeDown in brokeDowns) {
					string label;
					int value;
					if (brokeDown) {
						label = "Broke";
						value = 100;
					} else {
						label = "Didn\'t Break";
						value = 50;
					}

					breakDownSeries.Points.Add (new DataPoint (){ Label = "Match " + counter, Value = value });
					counter++;
				}


				foreach (var auto in autoCapabilities) {
					int value;

					switch (auto) {
					case "Never Moved":
						value = 10;
						break;
					case "In AutoZone":
						value = 20;
						break;
					case "1 Can":
						value = 30;
						break;
					case "1 Tote":
						value = 40;
						break;
					case "Tote Stack":
						value = 50;
						break;
					default:
						value = 0;
						break;
					}

					autoSeries.Points.Add (new DataPoint (){ Label = auto, Value = value });

				}

				//lineSeries.Points.Add(new DataPoint() { Label = "Jan",   Value = 27.5 });
				//lineSeries.Points.Add(new DataPoint() { Label = "Feb",   Value = 37.5 });
				//lineSeries.Points.Add(new DataPoint() { Label = "March", Value = 42.5 });


				Chart chart = new Chart () {
					Color = Color.Green,
					WidthRequest = HomePage.ScreenWidth - 10,
					HeightRequest = 250,
					Spacing = 0,
					VerticalOptions=LayoutOptions.FillAndExpand
				};

				chart.Series.Add (breakDownSeries);
				chart.Series.Add (autoSeries);
				chart.Series.Add (scoreSeries);
				StackLayout stack = new StackLayout ();


				//graphs.Children.Add(lbl);
				//graphs.Children.Add(chart);

				graphs.Children.Add (chart);
				graphs.Children.Remove (graphLoading);
				graphs.Children.Add (new Label{Text="Score", BackgroundColor=Color.Red, TextColor=Color.Black, FontSize = 15});
				graphs.Children.Add (new Label{Text="Reliability", BackgroundColor=Color.Blue, TextColor=Color.Black, FontSize = 15});
				graphs.Children.Add (new Label{Text="Auto Capabilities", BackgroundColor=Color.White, TextColor=Color.Black, FontSize = 15});


			} else {
				//await Navigation.PopModalAsync ();
				graphs.Children.Add ( new Label{Text = "Sorry, it looks like there aren\'t any reports for " + teamName} );
			}
		}

	}
}
