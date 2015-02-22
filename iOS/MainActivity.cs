using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace DataNuage.Sample.Android
{
    using System.Net;

    using DataNuage.Aws;

    [Activity(Label = "DataNuage.Sample.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var re =
                (HttpWebRequest)
                WebRequest.Create(string.Format("http://{0}.s3.amazonaws.com/{1}", "bucket", "name"));
            re.AllowWriteStreamBuffering = false;


            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += async delegate
            {
                button.Text = "In progress";
				string key = "AKIAIFT26MYLVAFP7QXQ";
				string secret = "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";
                var random = new Random();
                var s3 = new S3(key,secret);

				var bucket = "ahsscoutphotos";

                try
                {
                    await s3.CreateBucketAsync(bucket);
                    button.Text = String.Format("Bucket {0} created", bucket);

                    await s3.PutObjectAsync(bucket, "myobject", "Hello World");
                    button.Text = String.Format("Object myobject created");

                    var s = await s3.GetObjectAsStringAsync(bucket, "myobject");
                    button.Text = String.Format("{0} read", s);

                    await s3.DeleteObjectAsync(bucket, "myobject");
                    button.Text = String.Format("Object myobject deleted");

                    var dummyData = new byte[1000000];
                    await s3.PutObjectAsync(bucket, "big", dummyData, progress:l => button.Text = string.Format("Upload {0}%", (100 * l) / dummyData.Length));

                    await s3.DeleteObjectAsync(bucket, "big");

                    await s3.DeleteBucketAsync(bucket);
                    button.Text = String.Format("Empty bucket {0} deleted", bucket);


                    button.Text = "Success";
                }
                catch (S3Exception ex)
                {
                    new AlertDialog.Builder(this)
                        .SetPositiveButton("Ok", (_, __) => { })
                        .SetMessage(ex.Message)
                        .SetTitle("Error")
                        .Show();

                    button.Text = "Click me";
                }
            };
        }
    }
}