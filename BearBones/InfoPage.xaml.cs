using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BearBones
{	
	public partial class InfoPage : ContentPage
	{	
		public InfoPage (string name)
		{
			InitializeComponent ();
		}

		void OnDoneClicked (object sender, EventArgs e)
		{
			// leave this page
			Navigation.PopModalAsync ();
		}
	}
}

