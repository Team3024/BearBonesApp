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

		public ActivityIndicator spinningWheel;
		public List<string> allianceScore = new List<string> ();
		public List<string> autoCapabilities = new List<string> ();
		public List<bool> brokeDowns = new List<bool> ();
		public List<string> matchNumbers = new List<string> ();
		public List<string> noteses = new List<string> ();
		public List<bool> rebuildsStacks = new List<bool> ();
		public List<string> reportTypes = new List<string> ();
		public List<bool> setsContainerOnStacks = new List<bool> ();
		public List<bool> stacksToteses = new List<bool> ();
		public List<string> teamNames = new List<string> ();
		public List<int> teamNumbers = new List<int> ();
		public List<string> teamQualities = new List<string> ();
		public List<bool> yellowCoopStacks = new List<bool> ();
		public List<int> toteScores = new List<int> ();
		public List<int> canScores = new List<int> ();
		public List<int> coopScores = new List<int> ();
		public List<int> noodleScores = new List<int> ();

		public int teamNumber;
		public string teamName;
		public HomePageViewModel hp;
		public HomePage home;

		public InfoPage (HomePage h,HomePageViewModel hpvm)
		{
			home = h;
			if(hpvm != null)
				hp = hpvm;
			InitializeComponent ();
			getReports(hpvm.teamNumber);

			label1.TextColor = Color.FromRgb (170,170,170);
			label2.TextColor = Color.FromRgb (170,170,170);
			label3.TextColor = Color.FromRgb (170,170,170);


			teamNumber = hpvm.teamNumber;
			teamName = hpvm.teamName;

			Rest rest = new Rest ();
			var vid = String.Format ("https://www.google.com/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=youtube+frc+2015+PNW+{0}", teamNumber);
			//var vid = rest.queryYoutubeVideo (hpvm.teamNumber);
			webview.Source = vid;//hpvm.video;
			//title1.Text = hpvm.teamName + " " + hpvm.teamNumber + ": Rprt 1";



		}


		void OnDoneClicked (object sender, EventArgs e)
		{
			webview.Source = "";
			home.Refresh (sender,e);
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

			RemoveGraph ();

			graphLoading.IsVisible = true;


			Rest rest = new Rest ();
			Task <ObservableCollection<ReportViewModel>> list = rest.getReports (tNum);//SendAndReceiveJsonRequest ();
			var reports = await list;
			var count = 0;

			var pages = reports.Count;

			if (reports != null && reports.Count > 0)
			{
				teamNames.Clear ();
				teamNumbers.Clear ();
				matchNumbers.Clear ();
				noteses.Clear ();

				allianceScore.Clear ();
				toteScores.Clear ();
				canScores.Clear ();
				noodleScores.Clear ();
				coopScores.Clear ();

				autoCapabilities.Clear ();
				brokeDowns.Clear ();

				foreach (ReportViewModel ip in reports)
				{
					var p = ip;

					allianceScore.Add (ip.allianceScore);
					autoCapabilities.Add (ip.autoCapability);
					brokeDowns.Add (ip.brokeDown);
					matchNumbers.Add (ip.matchNumber);
					noteses.Add (ip.notes);
					teamNames.Add (ip.teamName);
					teamNumbers.Add (ip.teamNumber);
					toteScores.Add (ip.toteScore);
					canScores.Add (ip.canScore);
					coopScores.Add (ip.coopScore);
					noodleScores.Add (ip.noodleScore);


					if (hp != null) {
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

					this.Children.Add (report.CreatePage (

						teamNumber,
						teamName,
						ip.allianceScore,
						ip.matchNumber,
						ip.toteScore,
						ip.canScore,
						ip.noodleScore,
						ip.coopScore,
						ip.notes,
						ip.brokeDown,
						ip.autoCapability,
						count,
						pages));

					//this.Children.Add (report.CreatePage(ip.pointsScored, ip.driveType, ip.scoutName));



					//scores [count] = Convert.ToString (ip.score);
					//drives [count] = Convert.ToString (ip.drive);
					//scouts [count] = Convert.ToString (ip.scout);
					//scores.Add (Convert.ToString(ip.score));

					count++;

				}
				//Chart chart = await BuildGraphs ();
				//graphs.Children.Add (chart);
				await BuildGraphs ();
			} else {
				graphs.Children.Add (new Label{ Text="I'm sorry. There are no reports for this team.", XAlign = TextAlignment.Center });
			}
		}

		public async void Refresh(object sender, EventArgs e)
		{
			getReports(hp.teamNumber);

		}

		async void NewScoutReport(object sender, EventArgs e)
		{
			ScoutReportPage page = new ScoutReportPage (this,teamNumber.ToString());
			await Navigation.PushModalAsync (page);
		}

		BoxView CreateDivider()
		{
			return new BoxView{ WidthRequest = HomePage.ScreenWidth, HeightRequest = 2, BackgroundColor = Color.Black };
		}

		Label BuildSummary(string labelText, bool attribute)
		{
			var indicatorColor = Color.Black;
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



				for (var x = 0; x < toteScores.Count; x++) {

					int toteScoreInt;
					int canScoreInt;
					string toteScore = toteScores [x].ToString();
					string canScore = canScores [x].ToString();

					if (toteScore == null) {
						toteScoreInt = 0;
					} else {
						int.TryParse (toteScore, out toteScoreInt);
					}

					if (canScore == null) {
						canScoreInt = 0;
					} else {
						int.TryParse (canScore, out canScoreInt);
					}

					DataPoint dp = new DataPoint (){ Value = (toteScoreInt + canScoreInt) };

					scoreSeries.Points.Add (dp);

				}

				int counter = 1;
				foreach (var brokeDown in brokeDowns) {
					int value;
					string label;
					if (brokeDown) {
						label = "Broke";
						value = 50;
					} else {
						label = "Didn\'t Break";
						value = 100;
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
					Color = Color.Black,
					WidthRequest = HomePage.ScreenWidth - 10,
					HeightRequest = 350,
					Spacing = 0,
					VerticalOptions=LayoutOptions.FillAndExpand,
				};

				chart.Series.Add (breakDownSeries);
				chart.Series.Add (autoSeries);
				chart.Series.Add (scoreSeries);



				graphs.Children.Add (chart);
				//autoSeries.Points.Clear ();
				graphLoading.IsVisible = false;

				//graphs.Children.Add (new Label{Text="Composite Score", BackgroundColor=Color.Red, TextColor=Color.Black, FontSize = 15});
				//graphs.Children.Add (new Label{Text="Reliability", BackgroundColor=Color.Blue, TextColor=Color.Black, FontSize = 15});
				//graphs.Children.Add (new Label{Text="Auto Capabilities", BackgroundColor=Color.White, TextColor=Color.Black, FontSize = 15});

			} else {
				//await Navigation.PopModalAsync ();
				graphs.Children.Add ( new Label{Text = "Sorry, it looks like there aren\'t any reports for " + teamName} );
			}
				
		}

		void RemoveGraph(){
			int x = 0;

			foreach (View child in graphs.Children) {

				if (child.GetType () == typeof(Chart)) {
					graphs.Children.RemoveAt (x);
					Chart nc = (Chart)child;
					nc.Series.Clear ();
					break;
				}
				x++;
			}

		}

	}
}
