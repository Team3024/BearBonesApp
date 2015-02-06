using System;
using Xamarin.Forms;

namespace BearBones
{
	public class InfoCell
	{
		public InfoCell ()
		{



		}


		public ContentPage CreatePage(string teamScore, string teamDrive, string scoutName){
			var report = new ContentPage {
				Content = new StackLayout{
					Children = {
						new Label {Text = "Score: " + teamScore},
						new Label {Text = "Drive: " + Convert.ToString(teamDrive)},
						new Label {Text = "Scout: " + Convert.ToString(scoutName)},

					}
				}
			};

			return report;
		}

		//	protected override void OnBindingContextChanged ()
		//	{



		//	}
	}
}

