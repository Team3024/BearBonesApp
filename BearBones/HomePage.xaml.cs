using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Connectivity.Plugin;
using Connectivity.Plugin.Abstractions;
using System.Threading.Tasks;

namespace BearBones
{
	public partial class HomePage : ContentPage
	{
		public List<NewTeamPage> teams = new List<NewTeamPage> (); 
		ObservableCollection<HomePageViewModel> models;

		public HomePage()
		{
			// do the base class init();
			InitializeComponent();
			// create a new observable data list for scrolling
			models = new ObservableCollection<HomePageViewModel>();

			//TODO: if connected, fetch the team/reports data to populate the istView

			// set the ListView data source to empty list
			listView.ItemsSource = models;

		}

		void GetTeams()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				foreach (ConnectionType ct in CrossConnectivity.Current.ConnectionTypes)
				{// CrossConnectivity.Current.IsRemoteReachable ("71.92.131.203", 80, 5000).Result==true)
					if (ct == ConnectionType.WiFi || ct==ConnectionType.Cellular || ct==ConnectionType.Desktop)
					{
						// Get existing teams from database
						Rest rest = new Rest ();
						Task <ObservableCollection<InfoPageViewModel>> list = rest.SendAndReceiveJsonRequest ("http://71.92.131.203/db/data/cypher/", "MATCH (a:Team) RETURN a LIMIT 25");
						// populate the list with the results
						//listView.ItemsSource = list.Result;
						break;
					} else
					{
						// write it out for later processing
						models = models;
					}

				}
			}
		}

		public void newFRCTeam(string number, string name)
		{
			// get the current data source
			ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;

			// make a new team object
			HomePageViewModel m = new HomePageViewModel(typeof(NewTeamPage), Info, NewScoutReport, NewMatchReport);

			// set the name and team number
			m.teamName = name;
			m.teamNumber = number;
			m.PageName = name + " : " + number;
			m.index = models.Count + 1;

			// add this to our Home Page ListView
			home.Add (m);
			// reset the data source--this will trigger an update
			listView.ItemsSource = models;

		}

		async public void OnClick(object sender, EventArgs e)
		{

			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");

			NewTeamPage page = new NewTeamPage (this);
			await Navigation.PushModalAsync (page);
		}

		async void NavigateTo(Type pageType)
		{
			// Get all the constructors of the page type.
			IEnumerable<ConstructorInfo> constructors = 
				pageType.GetTypeInfo().DeclaredConstructors;

			foreach (ConstructorInfo constructor in constructors)
			{
				// Check if the constructor has no parameters.
				if (constructor.GetParameters().Length == 0)
				{
					// If so, instantiate it, and navigate to it.
					Page page = (Page)constructor.Invoke(null);
					await this.Navigation.PushAsync(page);
					break;
				}
			}
		}
			
		async void Info(string pageName)
		{
			InfoPage page = new InfoPage (pageName);
			await Navigation.PushModalAsync (page);
		}

		async void NewMatchReport(string pageName)
		{
			MatchReportPage page = new MatchReportPage (pageName);
			await Navigation.PushModalAsync (page);
		}
			
		async void NewScoutReport(string pageName)
		{
			ScoutReportPage page = new ScoutReportPage (pageName);
			await Navigation.PushModalAsync (page);
		}

		// Also go to the page when the ListView item is selected.
		void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			HomePageViewModel viewModel = args.SelectedItem as HomePageViewModel;

			if (viewModel != null)
			{
 				viewModel.InfoCommand.Execute(viewModel.PageType);
			}
		}

		internal void OnBindingContextChanged (object sender, EventArgs e)
		{
			var cell = (ViewCell)sender;
			//var template = Selector (cell.BindingContext);

			cell.Height = 300;
			//cell.View = ((ViewCell)template.CreateContent ()).View;
		}
	}

}

