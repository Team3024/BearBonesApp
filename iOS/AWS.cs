﻿using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using DataNuage.Aws;
using System.Net;
using System.IO;
using System.Drawing;
using XLabs.Platform.Services.Media;

namespace BearBones.iOS
{
	public class AWS : IAws
	{
		static string key = "AKIAIDJKIX3XN26PEZQA";
		static string secret = "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";

		byte[] imageData;// = new byte[10000000];
		public async void awsSaveFile(MediaFile img,string name)
		{
			var s3 = new S3(key,secret);

			var bucket = "ahsscoutphotos";

			MemoryStream mstr = new MemoryStream ();
			img.Source.CopyTo (mstr);
			imageData = mstr.ToArray ();

			try
			{
				try
				{

					await s3.CreateBucketAsync(bucket);
				//System.Diagnostics.Debug.WriteLine (String.Format("Bucket {0} created", bucket));
				}
				catch(Exception x)
				{
					System.Diagnostics.Debug.WriteLine (x.Message);
				}

				//await s3.PutObjectAsync(bucket, "myobject", "Hello World");
				//System.Diagnostics.Debug.WriteLine (String.Format("Object myobject created"));

				//var s = await s3.GetObjectAsStringAsync(bucket, "myobject");
				//System.Diagnostics.Debug.WriteLine (String.Format("{0} read", s));

				//await s3.DeleteObjectAsync(bucket, "myobject");
				//System.Diagnostics.Debug.WriteLine (String.Format("Object myobject deleted"));


				await s3.PutObjectAsync(bucket, name, imageData, progress: l => System.Diagnostics.Debug.WriteLine (string.Format("Upload {0}%", (100 * l) / imageData.Length)));

				//await s3.DeleteObjectAsync(bucket, name);

				//await s3.DeleteBucketAsync(bucket);
				//System.Diagnostics.Debug.WriteLine (String.Format("Empty bucket {0} deleted", bucket));

				System.Diagnostics.Debug.WriteLine ("Success");
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.Message);
				//return false;
			}
			//return true;
		}


		public async Task<ImageSource> awsGetFile(string name)
		{
			var s3 = new S3(key,secret);

			var bucket = "ahsscoutphotos";
			ImageSource imgSource=null;
			//name = "4f73e9c4-2296-4da9-83c7-5fba52dea117";
			try
			{
				Stream stream = await s3.GetObjectAsStreamAsync(bucket, name);
				imgSource = ImageSource.FromStream(() => stream);
				//img = new Image();
				//img.Source = imgSource;
				System.Diagnostics.Debug.WriteLine ("Success");
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.Message);
				//return false;
			}
			return imgSource;
		}

		public async void awsDeleteFile(string name)
		{
			var s3 = new S3(key,secret);

			var bucket = "ahsscoutphotos";

			try
			{
				await s3.DeleteObjectAsync(bucket, name);
				System.Diagnostics.Debug.WriteLine ("Success");
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.Message);
				//return false;
			}
			//return true;
		}
	}
}


