using DataNuage.Aws;
using System.Net;
using System;
using Xamarin.Forms;

namespace BearBones.Android
{
	public class AWS : IAws
	{
		public async void awsSaveFile(Image img,string name)
		{

			var random = new Random();

			var s3 = new S3("<Your AWS S3 Access Key Id> - ignored by Trial version",
				"<Your AWS S3 Secret Access Key> - ignored by Trial version");

			var bucket = "Team3024~" + random.Next();

			try
			{
				await s3.CreateBucketAsync(bucket);
				System.Diagnostics.Debug.WriteLine (String.Format("Bucket {0} created", bucket));

				await s3.PutObjectAsync(bucket, "myobject", "Hello World");
				System.Diagnostics.Debug.WriteLine (String.Format("Object myobject created"));

				var s = await s3.GetObjectAsStringAsync(bucket, "myobject");
				System.Diagnostics.Debug.WriteLine (String.Format("{0} read", s));

				await s3.DeleteObjectAsync(bucket, "myobject");
				System.Diagnostics.Debug.WriteLine (String.Format("Object myobject deleted"));

				var dummyData = new byte[1000000];
				await s3.PutObjectAsync(bucket, "big", dummyData, progress: l => System.Diagnostics.Debug.WriteLine (string.Format("Upload {0}%", (100 * l) / dummyData.Length)));

				await s3.DeleteObjectAsync(bucket, "big");

				await s3.DeleteBucketAsync(bucket);
				System.Diagnostics.Debug.WriteLine (String.Format("Empty bucket {0} deleted", bucket));

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


