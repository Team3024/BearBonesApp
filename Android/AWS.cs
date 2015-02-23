using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

using System.Net;
using System.IO;
using System.Drawing
;
using DataNuage.Aws;
using XLabs.Platform.Services.Media;

namespace BearBones.Android
{
	public class AWS : IAws
	{
		//static string key = "AWS KEY";
		//static string secret = "AWS Secret Key";
		string key = "AKIAJL3CI6IKYDMU2SQA";
		string secret = "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";

		public async void awsSaveFile(MediaFile img,string name,Label lbl)
		{
			byte[] imageData;// = new byte[10000000];
			var s3 = new S3(key,secret);

			var bucket = "ahsscoutphotos";

			MemoryStream mstr = new MemoryStream ();
			img.Source.CopyTo (mstr);
			imageData = mstr.ToArray ();

			try
			{
				try
				{

					//await s3.CreateBucketAsync(bucket);
					//System.Diagnostics.Debug.WriteLine (String.Format("Bucket {0} created", bucket));
				}
				catch(Exception x)
				{
					lbl.Text=x.Message;
					System.Diagnostics.Debug.WriteLine (x.Message);
				}

				//await s3.PutObjectAsync(bucket, "myobject", "Hello World");
				//System.Diagnostics.Debug.WriteLine (String.Format("Object myobject created"));

				//var s = await s3.GetObjectAsStringAsync(bucket, "myobject");
				//System.Diagnostics.Debug.WriteLine (String.Format("{0} read", s));

				//await s3.DeleteObjectAsync(bucket, "myobject");
				//System.Diagnostics.Debug.WriteLine (String.Format("Object myobject deleted"));


				await s3.PutObjectAsync(bucket, name, imageData,progress: l => lbl.Text = string.Format("Upload {0}%", (100 * l) / imageData.Length));

				//await s3.DeleteObjectAsync(bucket, name);

				//await s3.DeleteBucketAsync(bucket);
				//System.Diagnostics.Debug.WriteLine (String.Format("Empty bucket {0} deleted", bucket));
				lbl.Text="Upload Successful";
				System.Diagnostics.Debug.WriteLine ("Success");
			}
			catch (S3Exception ex)
			{
				lbl.Text=ex.Message;
				System.Diagnostics.Debug.WriteLine (ex.Message);
				//return false;
			}
			//return true;
		}


		public async void awsGetFile(string name,Label lbl,Image iv)
		{
			var s3 = new S3(key,secret);

			var bucket = "ahsscoutphotos";
			ImageSource imgSource=null;
			//name = "4f73e9c4-2296-4da9-83c7-5fba52dea117";
			try
			{
				lbl.Text="File downloading...";
				Stream stream = await s3.GetObjectAsStreamAsync(bucket, name);
				ImageSource imgSrc = ImageSource.FromStream(() => stream);
				iv.Source=imgSource;
				lbl.Text="Download Successful!";

				//img = new Image();
				//img.Source = imgSource;
				System.Diagnostics.Debug.WriteLine ("Success");
			}
			catch (S3Exception ex)
			{
				lbl.Text=ex.Message;
				System.Diagnostics.Debug.WriteLine (ex.Message);
				//return false;
			}
			//return imgSource;
		}

		public async void awsDeleteFile(string name,Label lbl)
		{
			var s3 = new S3(key,secret);

			var bucket = "ahsscoutphotos";

			try
			{
				await s3.DeleteObjectAsync(bucket, name);
				lbl.Text="File deleted";
				System.Diagnostics.Debug.WriteLine ("Success");
			}
			catch (S3Exception ex)
			{
				lbl.Text = ex.Message;
				System.Diagnostics.Debug.WriteLine (ex.Message);
				//return false;
			}
			//return true;
		}
	}
}


