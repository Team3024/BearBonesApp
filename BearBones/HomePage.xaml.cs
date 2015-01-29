using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Xamarin.Forms;
using Connectivity.Plugin;
using Connectivity.Plugin.Abstractions;
using System.Threading.Tasks;

namespace BearBones
{
	public partial class HomePage : ContentPage
	{
		bool bConnected=false;	//
		public List<NewTeamPage> teams = new List<NewTeamPage> (); 
		ObservableCollection<HomePageViewModel> models;//= new ObservableCollection<HomePageViewModel>();

		public HomePage()
		{
			// do the base class init();
			InitializeComponent();
			// create a new observable data list for scrolling
			models = new ObservableCollection<HomePageViewModel>();

			// get initial online status
			if (CrossConnectivity.Current.IsConnected)
			{
				bConnected = true;
				GetTeams ();
			}

			// if connected, fetch the team/reports data to populate the istView
			//CrossConnectivity.Current.ConnectivityChanged += OnClick ();
			// Lambda expression watches for connection status change
			CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
			{
				Debug.WriteLine("Connectivity Changed \nIsConnected: " + args.IsConnected.ToString());
				if(args.IsConnected == false)
				{
					// TODO: save any pending transactions, with timestamp
					bConnected=false;	// Set a flag to alert any transfers that may have ben in progress
				}
				else   // When state changes from false to true, fetch a fresh Mood set (TODO:and process unsaved changes...)
				{
					if(bConnected==false)
					{
						//TODO: process any saved transactions in chrono order by timestamp
						GetTeams();
					}
					bConnected=true;
				}
			};

			listView.ItemSelected += (sender, e) =>
			{
				if (e.SelectedItem == null) return; // don't do anything if we just de-selected the row
				// do something with e.SelectedItem
				//((ListView)sender).SelectedItem..IsEnabled=false;
				//((ListView)sender).match.IsEnabled=false;
				((ListView)sender).SelectedItem = null; // de-select the row
				HomePageViewModel hpvm = (HomePageViewModel) listView.SelectedItem;
				InfoPage page = new InfoPage (hpvm);// sent it the hpvm object
				Navigation.PushModalAsync (page);
			};
			// set the ListView data source to empty list
			listView.ItemsSource = models;

		}

		async void GetTeams()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				foreach (ConnectionType ct in CrossConnectivity.Current.ConnectionTypes)
				{// CrossConnectivity.Current.IsRemoteReachable ("71.92.131.203", 80, 5000).Result==true)
					if (ct == ConnectionType.WiFi || ct==ConnectionType.Cellular || ct==ConnectionType.Desktop)
					{
						// Get existing teams from database
						Rest rest = new Rest ();

						Task <ObservableCollection<HomePageViewModel>> list = rest.getAllTeams ();//SendAndReceiveJsonRequest ();
						var tList = await list;

						foreach (HomePageViewModel vm in tList) 
						{
							newFRCTeam (vm.teamNumber.ToString(),vm.teamName);
						}
						// populate the list with the results
						//listView.ItemsSource = list.Result;
						break;
					} else
					{
						// write it out for later processing
						//models = models;
					}

				}
			}
		}

		public void newFRCTeam(string number, string name)
		{
			// get the current data source
			ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;

			// make a new team object
			HomePageViewModel m = new HomePageViewModel(typeof(NewTeamPage));

			// set the name and team number
			m.teamName = name;
			int n;
			int.TryParse(number,out n);
			m.teamNumber=n;
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
			HomePageViewModel hpvm = (HomePageViewModel) listView.SelectedItem;
			InfoPage page = new InfoPage (hpvm);
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
				HomePageViewModel hpvm = (HomePageViewModel) listView.SelectedItem;
				InfoPage page = new InfoPage (hpvm);
				//await Navigation.PushModalAsync (page);viewModel.InfoCommand.Execute(viewModel.PageType);
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

