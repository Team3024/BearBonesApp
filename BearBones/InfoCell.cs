using System;
using Xamarin.Forms;

namespace BearBones
{
	public class InfoCell : ContentPage
	{



		public InfoCell ()
		{



		}


		public ContentPage CreatePage(string teamScore,
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

			for (int x = 0; x <= pageNumber; x++) {
				pageIndicator.Children.Add (new BoxView { BackgroundColor = Color.Black, WidthRequest = 5, HeightRequest = 5 });
			}


			var report = new ContentPage {

				Content = new ScrollView{ 
					Content = new StackLayout{
						BackgroundColor= new Color(0,0,0,0),
						Padding=5,
						Children = {
							new Label {Text = teamName + " " + teamNumber.ToString(), FontSize = 30, TextColor = Color.Black, XAlign = TextAlignment.Center},
							new Label {Text = "",
								HeightRequest=12},
							new Label {Text = "Score: " + teamScore, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Drive: " + teamDrive,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Scout: " + scoutName,
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
							new Label {Text = "Match Number: " + matchNumber,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							new Label {Text = "Notes: " + notes,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold, TextColor = Color.White},
							//Seperator
							new BoxView{WidthRequest=HomePage.ScreenWidth, HeightRequest=2, Color=Color.Black, BackgroundColor=Color.Black},
							//Robot Abilities
							new Label {Text="Robot:", FontSize=20, FontAttributes = FontAttributes.Bold},
							new Label {Text = "\t\t" + brokeDownText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + grabsContainerText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + grabsContainerOffStepText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + grabsToteText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + grabsToteOffStepText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + noodleBonusText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + noodleCleanupText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + noodleInContainerText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + rebuildStackText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + setsContainerOnStackText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + stacksTotesText, TextColor = Color.White,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t" + yellowCoopStackText, TextColor = Color.White,
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

