using System;
using System.Diagnostics;
using Xamarin.Forms;
using DataNuage.Aws;
using System.Net;

namespace BearBones.iOS
{
	public class AWS : IAws
	{
		byte[] dummyData = new byte[10000000];
		public async void awsSaveFile(Image img,string name)
		{
			var s3 = new S3("AKIAIDJKIX3XN26PEZQA",
				"iSYWws/7rOM9/b/gtD1R9f2ADg6vImprO3pN/y7q");

			var bucket = "ahsscoutphotos";

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


				await s3.PutObjectAsync(bucket, name, dummyData, progress: l => System.Diagnostics.Debug.WriteLine (string.Format("Upload {0}%", (100 * l) / dummyData.Length)));

				await s3.DeleteObjectAsync(bucket, name);

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
	}
}


