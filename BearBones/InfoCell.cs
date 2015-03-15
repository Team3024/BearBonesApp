using System;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;
using System.Threading.Tasks;

namespace BearBones
{
	public class InfoCell : ContentPage
	{



		public InfoCell ()
		{



		}


		public ContentPage CreatePage(
			int teamNumber,
			string teamName,
			string teamScore,
			string matchNumber,
			int toteScore,
			int canScore,
			int noodleScore,
			int coopScore,
			string notes,
			bool brokeDown,
			string autoCapabilities,
			int currentPage,
			int pageNumber)
		{

			string brokeDownText = (brokeDown) ? "broke down" : "didn\'t break down";
			StackLayout pageIndicator = new StackLayout{HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Horizontal};

			for (int x = 0; x < pageNumber; x++) {
				BoxView bv = new BoxView{ WidthRequest = 5, HeightRequest = 5, VerticalOptions = LayoutOptions.Center };
				if (currentPage == x) {
					bv.BackgroundColor = Color.White;
				} else {
					bv.BackgroundColor = Color.Black;
				}
				pageIndicator.Children.Add (bv);
			}

			var report = new ContentPage {
				BackgroundImage = "background.jpg",
				Content = new ScrollView{ 

					Content = new StackLayout{
						//BackgroundColor = Color.Red,
						Padding=5,
						Children = {
							new Label {Text = teamName + " " + teamNumber.ToString(), FontSize = 30, TextColor = Color.Black, FontAttributes= FontAttributes.Bold, XAlign = TextAlignment.Center},
							//new Image { Source=getPhoto(photo).Result},
							new Label {Text = "",
								HeightRequest=12},
							new Label {Text="Match:", FontSize=20, FontAttributes = FontAttributes.Bold, TextColor = Color.Black},
							new Label {Text = "Alliance Score: " + teamScore, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Tote Score: " + toteScore, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Can Score: " + canScore, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Total Individual Score: " + (toteScore + canScore).ToString(), TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							
							new Label {Text = "Auto Capabilities: " + autoCapabilities,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Match Number: " + matchNumber,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Notes: " + notes,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},

							pageIndicator
						}

					}
				}
			};

			return report;
		}

	}
}