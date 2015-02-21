using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using XLabs.Data;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

//using DataNuage.Aws;


/*
SCOUT REPORT

drive picker--swerve,mecanum,tank,other
build quality--junk,low,ok,high,superb
last year--prelim,regional,world
noodle in cont: yes/no
Cont on stack yes/no
noodle cleanup
noodle bonus
add photo

MATCH REPORT

Breakdown: yes/no
noodle in cont: yes/no
Cont on stack yes/no
Points scored: 0,5,10,20,30,40,50-200
coop stack: yes/no
auto: never moved,in-zone, 1 container, 1 tote, tote stack
grab containers off step: yes/no
grab totes off step: Yes/no
rebuild stacks.
noodle cleanup
noodle bonus
Team Rating: bad,poor,ok,good,superb
Notes:?

Save Report




*/
namespace BearBones
{	
	public partial class ScoutReportPage : ContentPage
	{	
		ReportViewModel model;
		InfoPage iPage;
		//IMediaPicker mediaPicker;

		string[] drvType = { "Mecanum", "Tank", "Swerve", "Other","none" };
		string[] allianceScor = {"0","5","10","20","30","40","50","60","70","80","90","100","150","200","250","none"};
		string[] toteScor = {"0","2","4","6","8","10","12"};
		string[] canScor = {"0","4","8","12","16","20","24","28","32"};
		string[] bldQuality = {"Superb","High","OK","Low","Poor","Junk","none"};
		string[] tmQuality = {"Superb","High","OK","Low","Poor","none"};
		string[] maxStak = {"0","1","2","3","4","5","6","none"};
		string[] autoCap = {"Never Moved","In AutoZone","1 Can","1 Tote","Tote Stack","none"};
		string[] lstYear = {"World","District","Prelims","none"};

		public ScoutReportPage (InfoPage ip,string number)
		{
			InitializeComponent ();
			iPage = ip;
			model = new ReportViewModel ();
			model.teamName = "???";
			int n;

			if (number != null) {
				int.TryParse (number, out n);
				model.teamNumber = n;
			}
			else
				model.teamNumber = 0;
			Random rand = new Random ();
			//model.pointsScored = rand.Next().ToString();
			model.matchNumber = rand.Next().ToString();
			model.reportType = "scout";

			canScore.SelectedIndexChanged += (object sender, EventArgs e) => 
			{
				Picker pik=(Picker)sender;
				canScore.Unfocus();
				model.canScore+=canScore.SelectedIndex*4;
				canScoreTitle.Text="Can Score:"+model.canScore.ToString();
				canScore.SelectedIndex=0;
			};

			toteScore.SelectedIndexChanged += (object sender, EventArgs e) => 
			{
				toteScore.Unfocus();
				model.toteScore+=toteScore.SelectedIndex*2;
				toteScore.SelectedIndex = 0;//On the screen picker stay at the old value
				toteScoreTitle.Text="Tote Score:"+model.toteScore.ToString();
				toteScore.SelectedIndex=0;
			};
			//model.scoutName = "JOHNNY FIVE";


		}

		private async Task PhotoAlbum (IMediaPicker mp)
		{
			//Setup ();
			try
			{
				var mediaFile = await mp.SelectPhotoAsync (new CameraMediaStorageOptions {
					DefaultCamera = CameraDevice.Rear,
					MaxPixelDimension = 400
				});
				var imgSource = ImageSource.FromStream(() => mediaFile.Source);
				this.img.Source = imgSource;

				var guid = System.Guid.NewGuid();
				if (imgSource != null) {

					model.photo=guid.ToString();
					DependencyService.Get<IPicture> ().SavePictureToDisk (imgSource,guid);
					DependencyService.Get<IAws>().awsSaveFile(mediaFile,guid.ToString());
					/*
					Task<ImageSource> result  = DependencyService.Get<IAws>().awsGetFile(guid.ToString());
					ImageSource source = await result;
					this.img.Source=source;
					*/
				}

			} catch (System.Exception ex)
			{
				Debug.WriteLine (ex.Message);
			}


		}

		private async Task CameraVideo (IMediaPicker mp)
		{
			//Setup ();
			try
			{
				var mediaFile = await mp.TakeVideoAsync (new VideoMediaStorageOptions {
					DefaultCamera = CameraDevice.Rear,
					MaxPixelDimension = 400
				});
				var imgSource = ImageSource.FromStream(() => mediaFile.Source);
				this.img.Source = imgSource;
				var guid = System.Guid.NewGuid();

			} catch (System.Exception ex)
			{
				Debug.WriteLine (ex.Message);
			}
		}

		private async Task CameraPhoto (IMediaPicker mp)
		{
			//Setup ();
			try
			{
				var mediaFile = await mp.TakePhotoAsync (new CameraMediaStorageOptions {
					DefaultCamera = CameraDevice.Rear,
					MaxPixelDimension = 400
				});
				var imgSource = ImageSource.FromStream(() => mediaFile.Source);
				this.img.Source = imgSource;
				var guid = System.Guid.NewGuid();
				model.photo=guid.ToString();
				DependencyService.Get<IPicture> ().SavePictureToDisk (imgSource,guid);
				DependencyService.Get<IAws>().awsSaveFile(mediaFile,guid.ToString());

			} catch (System.Exception ex)
			{
				Debug.WriteLine (ex.Message);
			}


		}

		private void harvestUIControls()
		{
			if(driveType.SelectedIndex>=0)
				model.driveType = drvType[driveType.SelectedIndex];
			else
				model.driveType = drvType[drvType.Length-1];

			if(allianceScore.SelectedIndex>=0)
				model.allianceScore = allianceScor [allianceScore.SelectedIndex];
			else
				model.allianceScore = allianceScor [allianceScor.Length-1];

			if(buildQuality.SelectedIndex>=0)
				model.buildQuality = bldQuality[buildQuality.SelectedIndex];
			else
				model.buildQuality = bldQuality[bldQuality.Length-1];

			if(autoCapability.SelectedIndex>=0)
				model.autoCapability = autoCap[autoCapability.SelectedIndex];
			else
				model.autoCapability = autoCap[autoCap.Length-1];

			if(maxStack.SelectedIndex>=0)
				model.maxStack = maxStak[maxStack.SelectedIndex];
			else
				model.maxStack = maxStak[maxStak.Length-1];

			if(teamQuality.SelectedIndex>=0)
				model.teamQuality = tmQuality[teamQuality.SelectedIndex];
			else
				model.teamQuality = tmQuality[tmQuality.Length-1];

			if(lastYear.SelectedIndex>=0)
				model.lastYearFinish = lstYear[lastYear.SelectedIndex];
			else
				model.lastYearFinish = lstYear[lstYear.Length-1];

			model.noodleInContainer = noodleInContainer.IsToggled;
			model.noodleCleanup = noodleCleanup.IsToggled;
			model.noodleBonus = noodleBonus.IsToggled;

			model.brokeDown = brokeDown.IsToggled;
			model.rebuildsStack = rebuildsStack.IsToggled;
			model.yellowCoopStack = yellowCoopStack.IsToggled;

			model.setsContainerOnStack = containerOnStack.IsToggled;
			model.grabsContainer = grabsContainer.IsToggled;
			model.grabsContainerOffStep = grabsContainerOffStep.IsToggled;

			model.stacksTotes = stacksTotes.IsToggled;
			model.grabsTote = grabsTote.IsToggled;
			model.grabsToteOffStep = grabsToteOffStep.IsToggled;
			model.notes = notes.Text;
			model.scoutName = scout.Text;
			model.matchNumber = match.Text;

		}

		void AddPhoto (object sender, EventArgs e)
		{
			var device = XLabs.Ioc.Resolver.Resolve<XLabs.Platform.Device.IDevice>();
			var mediaPicker = DependencyService.Get<XLabs.Platform.Services.Media.IMediaPicker> ()?? device.MediaPicker;
			PhotoAlbum (mediaPicker);
		}

		void TakePhoto (object sender, EventArgs e)
		{
			var device = XLabs.Ioc.Resolver.Resolve<XLabs.Platform.Device.IDevice>();
			var mediaPicker = DependencyService.Get<XLabs.Platform.Services.Media.IMediaPicker> ()?? device.MediaPicker;
			CameraPhoto (mediaPicker);
		}

		void TakeVideo (object sender, EventArgs e)
		{
			var device = XLabs.Ioc.Resolver.Resolve<XLabs.Platform.Device.IDevice>();
			var mediaPicker = DependencyService.Get<XLabs.Platform.Services.Media.IMediaPicker> ()?? device.MediaPicker;
			CameraVideo (mediaPicker);
		}

		void OnCancelClicked (object sender, EventArgs e)
		{
			model.canScore = 0;
			model.toteScore = 0;

			// leave this page
			Navigation.PopModalAsync ();
		}

		void OnDoneClicked (object sender, EventArgs e)
		{
			harvestUIControls();
			Rest rest = new Rest ();
			rest.createNewReport (model);
			//iPage.Refresh ();
			// leave this page
			Navigation.PopModalAsync ();
		}
	}
}

