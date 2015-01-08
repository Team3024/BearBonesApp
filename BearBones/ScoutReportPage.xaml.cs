using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BearBones
{	
	public partial class ScoutReportPage : ContentPage
	{	
		ScoutReportViewModel model;

		public ScoutReportPage (string number)
		{
			InitializeComponent ();
			model = new ScoutReportViewModel (number);

		}
		void OnDoneClicked (object sender, EventArgs e)
		{
			// leave this page
			Navigation.PopModalAsync ();
		}
	}
}

