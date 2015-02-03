using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BearBones
{	
	public partial class ScoutReportPage : ContentPage
	{	
		ReportViewModel model;

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
			model.score = rand.Next();
			model.driveType = "mecanum";
			model.matchNumber = rand.Next();
			model.reportType = "scout";
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

