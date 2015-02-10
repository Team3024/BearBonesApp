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


			var report = new ContentPage {

				Content = new ScrollView{ 
					Content = new StackLayout{
						BackgroundColor= new Color(0,0,0,0),
						Padding=5,
						Children = {

							new Label {Text = "",
								HeightRequest=12},
							new Label {Text = "Score: " + teamScore,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Drive: " + teamDrive,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Scout: " + scoutName,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Auto Capabilities: " + autoCapabilities,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text="Robot:"},
							new Label {Text = "\t\t\t\t" + brokeDownText, Style = Device.Styles.BodyStyle,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + grabsContainerText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + grabsContainerOffStepText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + grabsToteText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + grabsToteOffStepText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + noodleBonusText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + noodleCleanupText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + noodleInContainerText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + rebuildStackText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + setsContainerOnStackText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + stacksTotesText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "\t\t\t\t" + yellowCoopStackText,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Build Quality: " + buildQuality,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Team Quality: " + teamQuality,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Max Stack: " + maxStack,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Last Year Finish: " + lastYearFinish,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = "Match Number: " + matchNumber,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {HeightRequest=20},
							new Label {Text = "Notes: " + notes,
								//FontSize=25,
								FontAttributes= FontAttributes.Bold},
							new Label {Text = pageNumber.ToString(), AnchorY = HomePage.ScreenHeight - 20}
						}

					}
				}
			};

			return report;
		}

	}
}

