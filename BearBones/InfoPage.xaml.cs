using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BearBones
{	
	public partial class InfoPage : CarouselPage
	{	
	

		public InfoPage (string teamNumber)
		{

			InitializeComponent ();


		}

		void OnDoneClicked (object sender, EventArgs e)
		{
			// leave this page
			Navigation.PopModalAsync ();
		
		}

		//test for replacing value of Label
		void ChangeValue(object sender, EventArgs e)
		{

			ObservableCollection<InfoPageViewModel> lv = new ObservableCollection<InfoPageViewModel> ();

			Rest rest = new Rest();

			Task <ObservableCollection<InfoPageViewModel>> list = rest.SendAndReceiveJsonRequest ("http://71.92.131.203/db/data/cypher/","MATCH (a:Team) RETURN a LIMIT 25",lv);

			try {
				field1Entry.Text = int.Parse (field1Entry.Text) > 50 ? "impressive" : "could\'ve done better";
			} catch {
				field1Entry.Text = "I\'m sorry. Something didn\'t work.";
			}
		}
	}
}