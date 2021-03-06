﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
		public static int ScreenWidth;
		public static int ScreenHeight;

		public HomePage()
		{
			// do the base class init();
			InitializeComponent();
			this.BackgroundColor = Color.Red;
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
				InfoPage page = new InfoPage (this,hpvm);// sent it the hpvm object
				goInfo(page);
				page=page;

			};
			// set the ListView data source to empty list
			listView.ItemsSource = models;

		}

		async void goInfo(InfoPage page)
		{
			await Navigation.PushModalAsync (page);
			page=page;
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
							newFRCTeam (vm.teamNumber,vm.teamName,vm.swich);
						}
						/*
						var sortedOC = from item in models
							orderby item.teamName ascending
							select item;
*/
						models.Clear ();

						ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;


						foreach (HomePageViewModel item in tList.OrderBy((HomePageViewModel source)=>source.teamNumber))
						{
							item.PageName=item.teamNumber+"--"+item.teamName+"\nAuto:"+item.auto+"\nBroke:"+item.reliability;
							home.Add (item);
						}
						listView.ItemsSource = models;

						//foreach (var t in sortedOC.ase)
						//	models.Add (t);

						break;
					} else
					{
						// write it out for later processing
						//models = models;
					}

				}
			}
		}

		public async void PostTeam(int number, string name,string scout)
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				foreach (ConnectionType ct in CrossConnectivity.Current.ConnectionTypes)
				{// CrossConnectivity.Current.IsRemoteReachable ("71.92.131.203", 80, 5000).Result==true)
					if (ct == ConnectionType.WiFi || ct==ConnectionType.Cellular || ct==ConnectionType.Desktop)
					{
						// Get existing teams from database
						Rest rest = new Rest ();
						var str = await rest.createNewTeam (number,name,scout);
						break;
					} else
					{
						// write it out for later processing
						//models = models;
					}

				}
			}
		}


		public void newFRCTeam(int number, string name,bool sw)
		{
			// get the current data source
			ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;

			// make a new team object
			HomePageViewModel m = new HomePageViewModel(typeof(NewTeamPage));

			// set the name and team number
			m.teamName = name;
			m.teamNumber=number;
			m.PageName = name + " : " + number.ToString();
			m.index = models.Count + 1;
			m.swich = sw;
			// add this to our Home Page ListView
			home.Add (m);

			listView.ItemsSource = models;
		}

		async public void OnClick(object sender, EventArgs e)
		{

			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");

			NewTeamPage page = new NewTeamPage (this);
			await Navigation.PushModalAsync (page);
			page = page;
		}

		async public void Refresh(object sender, EventArgs e)
		{
			models.Clear ();
			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");
			// get initial online status
			if (CrossConnectivity.Current.IsConnected)
			{
				bConnected = true;
				GetTeams ();
			}
			// reset the data source--this will trigger an update
			SortByNum(null,null);
			//listView.ItemsSource = models;

		}

		async public void SortByName(object sender, EventArgs e)
		{

			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");


			ObservableCollection<HomePageViewModel> tList = new ObservableCollection<HomePageViewModel> ();
			foreach (HomePageViewModel vm in models)
				tList.Add (vm);

			models.Clear ();

			ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;



			//ObservableCollection<HomePageViewModel> removed = RemovedSelected (tList);

			Tuple<ObservableCollection<HomePageViewModel>, ObservableCollection<HomePageViewModel>> removed = RemovedSelected (tList);

			ObservableCollection<HomePageViewModel> sorted = new ObservableCollection<HomePageViewModel>(removed.Item1.OrderBy ((HomePageViewModel source) => source.teamName));

			ObservableCollection<HomePageViewModel> final = new ObservableCollection<HomePageViewModel>(sorted.Concat(removed.Item2));

			foreach (HomePageViewModel item in final)
			{
				item.PageName=item.teamName+"--"+item.teamNumber+"\nAuto:"+item.auto+"\nBroke:"+item.reliability;
				home.Add (item);
			}

			listView.ItemsSource = models;

		}


		public Tuple<ObservableCollection<HomePageViewModel>, ObservableCollection<HomePageViewModel>> RemovedSelected(ObservableCollection<HomePageViewModel> list)
		{
			ObservableCollection<HomePageViewModel> sort = new ObservableCollection<HomePageViewModel>();
			ObservableCollection<HomePageViewModel> unsort = new ObservableCollection<HomePageViewModel>();
			foreach(HomePageViewModel item in list){
				if (item.swich) {
					unsort.Add (item);
				} else {
					sort.Add (item);
				}
			}
			//sort = (ObservableCollection<HomePageViewModel>) sort.OrderBy ((HomePageViewModel source) => source.teamName);

			//sort = new ObservableCollection<HomePageViewModel>(sort.OrderBy((HomePageViewModel source) => source.teamName));

			//sort = new ObservableCollection<HomePageViewModel> (sort.Concat (unsort));

			//ObservableCollection<HomePageViewModel> end = (ObservableCollection<HomePageViewModel>) sort.Concat(unsort);
			return Tuple.Create (sort, unsort);
		}


		async public void SortByNum(object sender, EventArgs e)
		{

			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");


			ObservableCollection<HomePageViewModel> tList = new ObservableCollection<HomePageViewModel> ();
			foreach (HomePageViewModel vm in models)
				tList.Add (vm);

			models.Clear ();

			ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;



			Tuple<ObservableCollection<HomePageViewModel>, ObservableCollection<HomePageViewModel>> removed = RemovedSelected (tList);

			ObservableCollection<HomePageViewModel> sorted = new ObservableCollection<HomePageViewModel>(removed.Item1.OrderBy((HomePageViewModel source)=>source.teamNumber));

			ObservableCollection<HomePageViewModel> final = new ObservableCollection<HomePageViewModel>(sorted.Concat(removed.Item2));


			foreach (HomePageViewModel item in final)
			{
				item.PageName=item.teamNumber+"--"+item.teamName+"\nAuto:"+item.auto+"\nBroke:"+item.reliability;
				home.Add (item);
			}
			listView.ItemsSource = models;

		}

		async public void SortByToteScore(object sender, EventArgs e)
		{

			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");


			ObservableCollection<HomePageViewModel> tList = new ObservableCollection<HomePageViewModel> ();
			foreach (HomePageViewModel vm in models)
				tList.Add (vm);

			models.Clear ();

			ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;

			Tuple<ObservableCollection<HomePageViewModel>, ObservableCollection<HomePageViewModel>> removed = RemovedSelected (tList);

			ObservableCollection<HomePageViewModel> sorted = new ObservableCollection<HomePageViewModel>(removed.Item1.OrderBy((HomePageViewModel source)=>(1000-source.toteScore)));

			ObservableCollection<HomePageViewModel> final = new ObservableCollection<HomePageViewModel>(sorted.Concat(removed.Item2));

			foreach (HomePageViewModel item in final)
			{
				item.PageName=item.toteScore+"--"+item.teamName+"--"+item.teamNumber+"\nAuto:"+item.auto+"\nBroke:"+item.reliability;
				home.Add (item);
			}
			listView.ItemsSource = models;

		}

		async public void SortByCanScore(object sender, EventArgs e)
		{

			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");


			ObservableCollection<HomePageViewModel> tList = new ObservableCollection<HomePageViewModel> ();
			foreach (HomePageViewModel vm in models)
				tList.Add (vm);

			models.Clear ();

			ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;

			Tuple<ObservableCollection<HomePageViewModel>, ObservableCollection<HomePageViewModel>> removed = RemovedSelected (tList);

			ObservableCollection<HomePageViewModel> sorted = new ObservableCollection<HomePageViewModel>(removed.Item1.OrderBy((HomePageViewModel source)=>(1000-source.canScore)));

			ObservableCollection<HomePageViewModel> final = new ObservableCollection<HomePageViewModel>(sorted.Concat(removed.Item2));

			foreach (HomePageViewModel item in final)
			{
				item.PageName=item.canScore+"--"+item.teamName+"--"+item.teamNumber+"\nAuto:"+item.auto+"\nBroke:"+item.reliability;
				home.Add (item);
			}
			listView.ItemsSource = models;

		}

		async public void SortByCoopScore(object sender, EventArgs e)
		{

			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");


			ObservableCollection<HomePageViewModel> tList = new ObservableCollection<HomePageViewModel> ();
			foreach (HomePageViewModel vm in models)
				tList.Add (vm);

			models.Clear ();

			ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;

			Tuple<ObservableCollection<HomePageViewModel>, ObservableCollection<HomePageViewModel>> removed = RemovedSelected (tList);

			ObservableCollection<HomePageViewModel> sorted = new ObservableCollection<HomePageViewModel>(removed.Item1.OrderBy((HomePageViewModel source)=>(1000-source.coopScore)));

			ObservableCollection<HomePageViewModel> final = new ObservableCollection<HomePageViewModel>(sorted.Concat(removed.Item2));

			foreach (HomePageViewModel item in final)
			{
				item.PageName=item.coopScore+"--"+item.teamName+"--"+item.teamNumber+"\nAuto:"+item.auto+"\nBroke:"+item.reliability;
				home.Add (item);
			}
			listView.ItemsSource = models;

		}

		async public void SortByNoodleScore(object sender, EventArgs e)
		{

			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");


			ObservableCollection<HomePageViewModel> tList = new ObservableCollection<HomePageViewModel> ();
			foreach (HomePageViewModel vm in models)
				tList.Add (vm);

			models.Clear ();

			ObservableCollection<HomePageViewModel> home = listView.ItemsSource as ObservableCollection<HomePageViewModel>;

			Tuple<ObservableCollection<HomePageViewModel>, ObservableCollection<HomePageViewModel>> removed = RemovedSelected (tList);

			ObservableCollection<HomePageViewModel> sorted = new ObservableCollection<HomePageViewModel>(removed.Item1.OrderBy((HomePageViewModel source)=>(1000-source.noodleScore )));

			ObservableCollection<HomePageViewModel> final = new ObservableCollection<HomePageViewModel>(sorted.Concat(removed.Item2));

			foreach (HomePageViewModel item in final)
			{
				item.PageName=item.noodleScore+"--"+item.teamName+"--"+item.teamNumber+"\nAuto:"+item.auto+"\nBroke:"+item.reliability;
				home.Add (item);
			}
			listView.ItemsSource = models;

		}
		/*
		public void FilterTeams(string text)
		{
			model.Clear();
			var sorted = from team in model
				orderby group monkey 
					by monkey.NameSort into monkeyGroup
						select new Grouping<string, Monkey>(monkeyGroup.Key, monkeyGroup);
		}

		public string NameSort
		{
			get
			{ 
				if (string.IsNullOrWhiteSpace(Name) || Name.Length == 0)
					return "?";

				return Name[0].ToString().ToUpper();
			}
		}

		public int NumSort
		{
			get
			{ 
				if (SortByNum )
					return -1;

				return Name[0].ToString().ToUpper();
			}
		}
*/
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
			InfoPage page = new InfoPage (this,hpvm);
			await Navigation.PushModalAsync (page);
			hpvm = hpvm;
		}

		// Also go to the page when the ListView item is selected.
		void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			HomePageViewModel viewModel = args.SelectedItem as HomePageViewModel;

			if (viewModel != null)
			{
				HomePageViewModel hpvm = (HomePageViewModel) listView.SelectedItem;
				InfoPage page = new InfoPage (this,hpvm);
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

