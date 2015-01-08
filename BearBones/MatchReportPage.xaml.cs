using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BearBones
{	
	public partial class MatchReportPage : ContentPage
	{	
		MatchReportViewModel model; 

		public MatchReportPage (string number)
		{
			InitializeComponent ();
			model = new MatchReportViewModel(number);

		}
		void OnDoneClicked (object sender, EventArgs e)
		{
			// leave this page
			Navigation.PopModalAsync ();
		}
	}
}

