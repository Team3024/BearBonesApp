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

		async Task<ImageSource> getPhoto(string name)
		{
			if (name == null)
				return null;
			Task<ImageSource> result = DependencyService.Get<IAws> ().awsGetFile (name);
			//return await result;
			ImageSource source = await result;
			return source;
		}

		public ContentPage CreatePage(string teamScore,
			int toteScore,
			int canScore,
			string teamDrive,
			string scoutName,
			string autoCapabilities,
			bool brokeDown,
			string buildQuality,
			bool grabsContainer,
			bool grabsContainerOffStep,
			bool grabsTote,
			bool grabsToteOffStep,
			string lastYearFinish,
			string matchNumber,
			string maxStack,
			bool noodleBonus,
			bool noodleCleaup,
			bool noodleInContainer,
			string notes,
			bool rebuildStack,
			string reportType,
			bool setsContainerOnStack,
			bool stacksTotes,
			string teamName,
			int teamNumber,
			string teamQuality,
			bool yellowCoopStack,
			string photo,
			int currentPage,
			int pageNumber)
		{



			string brokeDownText = (brokeDown) ? "broke down" : "didn\'t break down";
			string grabsContainerText = (grabsContainer) ? "grabs containers" : "can\'t grab containers";
			string grabsContainerOffStepText = (grabsContainerOffStep) ? "grabs containers off step" : "can\'t grab containers off step";
			string grabsToteText = (grabsTote) ? "grabs totes" : "can\'t grab totes";
			string grabsToteOffStepText = (grabsToteOffStep) ? "grabs tote off step" : "can\'t grab tote off step";
			string noodleBonusText = (noodleBonus) ? "executes noodle bonus" : "can\'t execute noodle bonus";
			string noodleCleanupText = (noodleCleaup) ? "cleans up noodles" : "can\'t clean up noodles";
			string noodleInContainerText = (noodleInContainer) ? "puts noodle in container" : "can\'t put noodle in container";
			string rebuildStackText = (rebuildStack) ? "rebuilds stacks" : "can\'t rebuild stacks";
			string setsContainerOnStackText = (setsContainerOnStack) ? "sets containers on stacks" : "can\'t set containers on stacks";
			string stacksTotesText = (stacksTotes) ? "stackes totes" : "can\'t stack totes";
			string yellowCoopStackText = (yellowCoopStack) ? "execute co-op stack" : "can\'t execute co-op stack";

		

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
							new Label {Text = "Drive: " + teamDrive,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Auto Capabilities: " + autoCapabilities,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Build Quality: " + buildQuality,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Team Quality: " + teamQuality,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Max Stack: " + maxStack,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Last Year Finish: " + lastYearFinish,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Scout: " + scoutName,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Match Number: " + matchNumber,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Notes: " + notes,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							//Seperator
							new BoxView{WidthRequest=HomePage.ScreenWidth, HeightRequest=2, Color=Color.Black, BackgroundColor=Color.Black},
							//Robot Abilities
							new Label {Text="Robot:", FontSize=20, FontAttributes = FontAttributes.Bold, TextColor = Color.Black},
							new Label {Text = brokeDownText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = grabsContainerText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = grabsContainerOffStepText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = grabsToteText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = grabsToteOffStepText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = noodleBonusText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = noodleCleanupText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = noodleInContainerText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = rebuildStackText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = setsContainerOnStackText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = stacksTotesText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = yellowCoopStackText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							pageIndicator
						}

					}
				}
			};

			return report;
		}

	}
}