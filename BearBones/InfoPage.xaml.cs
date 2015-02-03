using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BearBones
{	



	public partial class InfoPage : CarouselPage
	{	

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
			Task <ObservableCollection<InfoPageViewModel>> list =  rest.getReports (tNum);//SendAndReceiveJsonRequest ();
			var reports = await list;
			//var count = 0;
			List<string> scores = new List<string> ();
			List<string> drives = new List<string> ();
			List<string> scouts = new List<string> ();
			foreach (InfoPageViewModel ip in reports) 
			{
				var p=ip;
				//scoreEntry.Text = Convert.ToString (ip.score);
				//driveEntry.Text = Convert.ToString (ip.drive);
				//scoutEntry.Text = Convert.ToString (ip.scout);

				scores.Add (Convert.ToString (ip.score));
				drives.Add (Convert.ToString (ip.drive));
				scouts.Add (Convert.ToString (ip.scout));

				InfoCell report = new InfoCell ();

				this.Children.Add (report.CreatePage(ip.score, ip.drive, ip.scout));



				//scores [count] = Convert.ToString (ip.score);
				//drives [count] = Convert.ToString (ip.drive);
				//scouts [count] = Convert.ToString (ip.scout);
				//scores.Add (Convert.ToString(ip.score));

				//count++;

			}


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

	}
}