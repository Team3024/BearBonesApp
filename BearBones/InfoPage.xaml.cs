using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BearBones
{	
	public partial class InfoPage : CarouselPage
	{	
	
		string[] scores;
		string[] drives;
		string[] scouts;

		public InfoPage (HomePageViewModel hpvm)
		{

			InitializeComponent ();
			getReports(hpvm.teamNumber);

			title1.Text = hpvm.teamName + " " + hpvm.teamNumber + ": Rprt 1";


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
			var count = 0;
			foreach (InfoPageViewModel ip in reports) 
			{
				var p=ip;
				scoreEntry.Text = Convert.ToString (ip.score);
				driveEntry.Text = Convert.ToString (ip.drive);
				scoutEntry.Text = Convert.ToString (ip.scout);

				//scores [count] = Convert.ToString (ip.score);
				//drives [count] = Convert.ToString (ip.drive);
				//scouts [count] = Convert.ToString (ip.scout);

				//NEED TO FIGURE THIS OUT
			
				//count++;

			}

	
		}




	}
}