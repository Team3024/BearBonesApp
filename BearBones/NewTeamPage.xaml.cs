using System;
using Xamarin.Forms;
using System.Collections.Generic;


namespace BearBones
{
	public partial class NewTeamPage : ContentPage
	{
		internal int teamNumber {  set; get; }
		internal string teamName {  set; get; }
		internal NewTeamViewModel viewModel { get; set; }
		internal HomePage parent { get; set; }
		internal List<ScoutingReport> reports;

		public NewTeamPage (HomePage parent)
		{
			try
			{
				// do whatever is basic to this objects
				InitializeComponent ();

				// buttons and things are bound to this model
				viewModel = new NewTeamViewModel ();
				BindingContext = viewModel;

				// this is the Home Page and holds the UI state machine
				this.parent = parent;

				// create an empty list of reports
				reports = new List<ScoutingReport>();
			}
			catch(Exception ex)
			{
				Exception e = ex;
			}
		}

		void OnDoneClicked (object sender, EventArgs e)
		{
			// get the input values
			teamNumber = viewModel.teamNumber;
			teamName = viewModel.teamName;

			// callback to main thread to add a team and update UI
			parent.newFRCTeam (teamNumber, teamName,false);

			parent.PostTeam(teamNumber, teamName,viewModel.scoutName);
			// leave this page
			Navigation.PopModalAsync ();
		}

		void OnCancelClicked (object sender, EventArgs e)
		{
			//just leave
			Navigation.PopModalAsync ();
		}


	}
}


