using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace BearBones
{
	public partial class HomePage
	{
		public HomePage()
		{
			InitializeComponent();

			ObservableCollection<HomePageViewModel> models = new ObservableCollection<HomePageViewModel>
			{
				//new HomePageViewModel(typeof(ClockPage), NavigateTo, BrowseSource)
				/*
				new HomePageViewModel(typeof(HelloXamlPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(XamlPlusCodePage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(GridDemoPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(AbsoluteDemoPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(SharedResourcesPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(StaticConstantsPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(RelativeLayoutPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(SliderBindingsPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(SliderTransformsPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(ListViewDemoPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(OneShotDateTimePage), NavigateTo, BrowseSource),

				new HomePageViewModel(typeof(HslColorScrollPage), NavigateTo, BrowseSource),
				new HomePageViewModel(typeof(KeypadPage), NavigateTo, BrowseSource)
*/
			};

			listView.ItemsSource = models;
		}

		void newFRCTeam()
		{
			ObservableCollection<HomePageViewModel> mods = (ObservableCollection<HomePageViewModel>)listView.ItemsSource;//as List<HomePageViewModel>;
			HomePageViewModel m = new HomePageViewModel(typeof(NewTeamPage), NavigateTo, BrowseSource);
			mods.Add (m);
			listView.ItemsSource = mods;

		}

		async void OnClick(object sender, EventArgs e)
		{

			//ToolbarItem tbi = (ToolbarItem) sender;
			//this.DisplayAlert("Selected!", tbi.Name,"OK");
			//await this.Navigation.PushAsync(Type "newTeam");
			//var page = new newFRCTeam(this);
			//await Navigation.PushModalAsync(page);
			//await Navigation.PopAsync ();
			newFRCTeam ();
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

		async void BrowseSource(string pageName)
		{
			string xamlPage = "XamlSamples." + pageName + ".xaml";
			Assembly assembly = this.GetType().GetTypeInfo().Assembly;

			using (Stream stream = assembly.GetManifestResourceStream(xamlPage))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					string xaml = reader.ReadToEnd();
					//await this.Navigation.PushAsync(new XamlBrowserPage(xaml));
				}
			}
		}

		// Also go to the page when the ListView item is selected.
		void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			HomePageViewModel viewModel = args.SelectedItem as HomePageViewModel;

			if (viewModel != null)
			{
				viewModel.GoToCommand.Execute(viewModel.PageType);
			}
		}
	}
}

