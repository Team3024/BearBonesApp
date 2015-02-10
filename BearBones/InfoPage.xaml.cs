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

		public List<string> pointsScoreds = new List<string> ();
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

		public InfoPage (HomePageViewModel hpvm)
		{

			InitializeComponent ();
			getReports(hpvm.teamNumber);

			teamNumber = hpvm.teamNumber;

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


			foreach (ReportViewModel ip in reports) 
			{
				var p=ip;

				pointsScoreds.Add (ip.pointsScored);
				driveTypes.Add (ip.driveType);
				scoutNames.Add (ip.scoutName);
				autoCapabilities.Add(ip.autoCapability);
				brokeDowns.Add(ip.brokeDown);
				buildQualities.Add(ip.buildQuality);
				grabsContainers.Add(ip.grabsContainer);
				grabsContainerOffSteps.Add(ip.grabsContainerOffStep);
				grabsTotes.Add(ip.grabsTote);
				grabsToteOffSteps.Add(ip.grabsToteOffStep);
				lastYearFinishes.Add(ip.lastYearFinish);
				matchNumbers.Add(ip.matchNumber);
				maxStacks.Add(ip.maxStack);
				noodleBonuses.Add(ip.noodleBonus);
				noodleCleanups.Add(ip.noodleCleanup);
				noodleInContainers.Add(ip.noodleInContainer);
				noteses.Add(ip.notes);
				rebuildsStacks.Add(ip.rebuildsStack);
				reportTypes.Add(ip.reportType);
				setsContainerOnStacks.Add(ip.setsContainerOnStack);
				stacksToteses.Add(ip.stacksTotes);
				teamNames.Add(ip.teamName);
				teamNumbers.Add(ip.teamNumber);
				teamQualities.Add(ip.teamQuality);
				yellowCoopStacks.Add(ip.yellowCoopStack);

				InfoCell report = new InfoCell ();

				this.Children.Add (report.CreatePage(ip.pointsScored,
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

			BuildGraphs ();


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

		async void BuildGraphs()
		{
			Series firstBarSeries = new Series
			{
				Color = Color.Red,
				Type = ChartType.Bar
			};
			firstBarSeries.Points.Add(new DataPoint() { Label = "Jan",   Value = 25});
			firstBarSeries.Points.Add(new DataPoint() { Label = "Feb",   Value = 40});
			firstBarSeries.Points.Add(new DataPoint() { Label = "March", Value = 45});

			Series secondBarSeries = new Series
			{
				Color = Color.Blue,
				Type = ChartType.Bar
			};
			secondBarSeries.Points.Add(new DataPoint() { Label = "Jan",   Value = 30 });
			secondBarSeries.Points.Add(new DataPoint() { Label = "Feb",   Value = 35 });
			secondBarSeries.Points.Add(new DataPoint() { Label = "March", Value = 40 });

			Series lineSeries = new Series
			{
				Color = Color.Green,
				Type = ChartType.Line
			};
			lineSeries.Points.Add(new DataPoint() { Label = "Jan",   Value = 27.5 });
			lineSeries.Points.Add(new DataPoint() { Label = "Feb",   Value = 37.5 });
			lineSeries.Points.Add(new DataPoint() { Label = "March", Value = 42.5 });


			Chart chart = new Chart()
			{
				Color = Color.White,
				WidthRequest = HomePage.ScreenWidth-10,
				HeightRequest = 100,
				Spacing = 10
			};
			chart.Series.Add(firstBarSeries);
			chart.Series.Add(secondBarSeries);
			chart.Series.Add(lineSeries);
			StackLayout stack = new StackLayout ();

			Label lbl = new Label {
				Text = ""
			};

			graphs.Children.Add(lbl);
			graphs.Children.Add(chart);


		}

	}
}