using System;
using Xamarin.Forms;

namespace BearBones
{
	public class TeamCell: ViewCell
	{

		public bool isChosen = false;
		public int index { set; get; }

		public TeamCell ()
		{
			var temp = BindingContext as HomePageViewModel;

		}


		// this gets called after the object is instantiated but before it is displayed
		// you can override the object contents here
		// that means each cell in the ListView can have different contents--in this case based upon index
		protected override void OnBindingContextChanged ()
		{
			//make this cell a particular height, based on its index
			this.Height = 60;
			base.OnBindingContextChanged ();
			var temp = BindingContext as HomePageViewModel;
			Switch swich = new Switch();



			if (temp.isChosen) {
				swich.IsToggled = true;
			}

			swich.Toggled  += (object sender, ToggledEventArgs e) => {
				if(swich.IsToggled){temp.isChosen = true;}else{temp.isChosen = false;}
			};

			Label title = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				TextColor = Color.White,
				BackgroundColor = Color.Red,
				Opacity = .75,
				FontSize = 14,
				HeightRequest = 60
			};


			title.SetBinding (Label.TextProperty, new Binding ("PageName"));
			if (temp.PageName.Contains ("Broke:True")) {
				title.BackgroundColor = Color.Yellow;
				title.TextColor = Color.Black;
			}
			else if(temp.reports==1)
			{
				title.BackgroundColor = Color.Red;
				title.TextColor = Color.White;
			}
			else
			{
				title.BackgroundColor = Color.White;
				title.TextColor = Color.Black;
			}

			var s = new StackLayout
			{
				Orientation=StackOrientation.Horizontal,
				Padding = new Thickness(5,0,5,0)
			};


			s.Children.Add (swich);

			s.Children.Add (title);
			this.View = s;
		}

	}

}