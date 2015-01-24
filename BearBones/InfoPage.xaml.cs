using System;
using System.Collections.Generic;
using Xamarin.Forms;


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
			field1Entry.Text = "lots and lots";
		}
	}
}