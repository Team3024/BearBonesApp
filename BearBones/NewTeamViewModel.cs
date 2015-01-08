using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BearBones
{
	public class NewTeamViewModel : ViewModelBase
	{
		public string teamNumber { get; set; }
		public string teamName { get; set; }
		public string scoutName { get; set; }
		internal string ValidationErrors { get; private set; }

		//		IDirectoryService service;

		static readonly TimeSpan ForceLoginTimespan = TimeSpan.FromMinutes (5);

		public NewTeamViewModel ()
		{
			this.teamNumber = "";
			this.teamName = "";
			this.scoutName = "";
		}
		/*
		public IDirectoryService Service { get; set;}

		public NewTeamViewModel (IDirectoryService service)
		{
			Service = service;

			Username = "";
			Password = "";
		}
		*/
		public bool CanLogin () {
			ValidationErrors = "";
			if (string.IsNullOrEmpty(teamNumber))
			{
				ValidationErrors = "Please enter a Team Number.";
			}
			if (string.IsNullOrEmpty(teamName))
			{
				ValidationErrors += "Please enter a Team Name.";
			}
			return (ValidationErrors == "");
		}

	}
}


