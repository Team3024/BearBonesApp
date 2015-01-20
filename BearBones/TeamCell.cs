using System;
using Xamarin.Forms;

namespace BearBones
{
	public class TeamCell: ViewCell
	{
		public int index { set; get; }

		public TeamCell ()
		{
			var temp = BindingContext as HomePageViewModel;


			// Populating the ViewCell is now deferred to OnBindingContextChanged () below
			// This lets us customize things like size and widget content for each cell

			/*
			Label title = new Label
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			title.SetBinding (Label.TextProperty, new Binding("PageName"));


			Button infoBtn = new Button
			{
				Text="Info",
				//Command="InfoCommand",
				CommandParameter="teamNumber"
				
			};

			//infoBtn.SetBinding (Button.Command, new Binding("InfoCommand"));
			//infoBtn.SetBinding (Button.CommandParameter,new Binding("teamNumber"));

			Button scoutBtn = new Button
			{
				Text="New Scout Report",
				//Command="ScoutReportCommand",
				CommandParameter="teamNumber"
			};

			//scoutBtn.SetBinding (Button.Command, "ScoutReportCommand");
			//scoutBtn.SetBinding (Button.CommandParameter, "teamNumber");

			Button matchBtn = new Button
			{
				Text="New Match Report",
				//Command="ScoutReportCommand",
				CommandParameter="teamNumber"
			};
			//matchBtn.SetBinding (Button.Command, "MatchReportCommand");
			//matchBtn.SetBinding (Button.CommandParameter, "teamNumber");

			var s = new StackLayout
			{
				Orientation=StackOrientation.Horizontal,
				Padding = new Thickness(5,0,5,0)
			};
			s.Children.Add (title);
			s.Children.Add (infoBtn);
			s.Children.Add (scoutBtn);
			s.Children.Add (matchBtn);
			this.View = s;
			*/
		}


		// this gets called after the object is instantiated but before it is displayed
		// you can override the object contents here
		// that means each cell in the ListView can have different contents--in this case based upon index
		protected override void OnBindingContextChanged ()
		{
			//make this cell a particular height, based on its index
			//this.Height = temp.index*30;

			base.OnBindingContextChanged ();
			var temp = BindingContext as HomePageViewModel;
			Label title = new Label
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			title.SetBinding (Label.TextProperty, new Binding("PageName"));


			Button infoBtn = new Button
			{
				Text="Info",
				//Command="InfoCommand",
				CommandParameter="teamNumber"
			};
			infoBtn.SetBinding<HomePageViewModel> (Button.CommandProperty,f => f.InfoCommand);
			infoBtn.SetBinding<HomePageViewModel> (Button.CommandParameterProperty,f => f.teamNumber);

			Button scoutBtn = new Button
			{
				Text="New Scout Report",
				//Command="ScoutReportCommand",
				CommandParameter="teamNumber"
			};

			scoutBtn.SetBinding<HomePageViewModel> (Button.CommandProperty,f => f.ScoutReportCommand);
			infoBtn.SetBinding<HomePageViewModel> (Button.CommandParameterProperty,f => f.teamNumber);

			Button matchBtn = new Button
			{
				Text="New Match Report",
				//Command="ScoutReportCommand",
				CommandParameter="teamNumber"
			};
			matchBtn.SetBinding<HomePageViewModel> (Button.CommandProperty,f => f.MatchReportCommand);
			infoBtn.SetBinding<HomePageViewModel> (Button.CommandParameterProperty,f => f.teamNumber);

			var s = new StackLayout
			{
				Orientation=StackOrientation.Horizontal,
				Padding = new Thickness(5,0,5,0)
			};
			s.Children.Add (title);
			s.Children.Add (infoBtn);
			s.Children.Add (scoutBtn);
			s.Children.Add (matchBtn);
			this.View = s;


		}

	}

}

/*
          <? <ViewCell> 
          <StackLayout Orientation="Horizontal"
                       Padding="5, 0"
                       >
            <Label Text="{Binding PageName}"
                   HorizontalOptions="StartAndExpand"
                   VerticalOptions="Center" />

            <Button Text="Info"
                    Command="{Binding InfoCommand}"
                    CommandParameter="{Binding teamNumber}" />

            <Button Text="New Scout Report"
                    Command="{Binding ScoutReportCommand}"
                    CommandParameter="{Binding teamNumber}" />
            
            <Button Text="New Match Report"
                    Command="{Binding MatchReportCommand}"
                    CommandParameter="{Binding teamNumber}" />

          </StackLayout>
        </ViewCell> ?>
 */
