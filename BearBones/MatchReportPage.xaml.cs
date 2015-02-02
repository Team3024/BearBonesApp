using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BearBones
{	
	public partial class MatchReportPage : ContentPage
	{	
		ReportViewModel model; 

		public MatchReportPage (string number)
		{
			InitializeComponent ();
			model = new ReportViewModel(number);

		}
		void OnDoneClicked (object sender, EventArgs e)
		{
			// leave this page
			Navigation.PopModalAsync ();
		}
	}
}

