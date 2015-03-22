using System;
using Xamarin.Forms;

namespace BearBones
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			return new NavigationPage(new HomePage ());
		}
	}
}

