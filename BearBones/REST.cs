using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;

using Microsoft.CSharp;
using ModernHttpClient;

using System.Linq;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;



/// <summary>
///  Calling the REST interface
///  Rest rest = new Rest ();
///  Task <List<string>> list = rest.SendAndReceiveJsonRequest ("http://71.92.131.203/db/data/cypher/","MATCH (a:Event) RETURN a LIMIT 25",lv);
/// </summary>


namespace BearBones
{
	public class Rest
	{
		public Rest ()
		{
		}

		public async Task<List<string>> SendAndReceiveJsonRequest(string uri, string query,ListView list)
		{
			string responseStr = null;
			//string uri = "http://71.92.131.203/db/data/cypher/";

			// Create a json string with a single key/value pair.

			var json = new JObject(new JProperty("query",query)).ToString();

			using (var httpClient = new HttpClient())
			{        
				//create the http request content

				HttpContent content = new StringContent(json);

				try
				{
					// Send the json to the server using POST

					Task<HttpResponseMessage> getResponse = httpClient.PostAsync(uri, content);
					// Wait for the response and read it to a string var

					HttpResponseMessage response = await getResponse;
					responseStr = await response.Content.ReadAsStringAsync();
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Error communicating with the server: " + e.Message);
				}
			}
			Dictionary<string,int[]> D = new Dictionary<string,int[]>();
			var results = new List<string>();
			try
			{
				//Dictionary<string, dynamic> values = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseStr);
				neo4jData val=Newtonsoft.Json.JsonConvert.DeserializeObject<neo4jData>(responseStr);

				foreach(var kvp in val.data)// process column 'a'
				{
					foreach(var kv in kvp)
					{
						foreach(var k in kv)
						{
							if(k.Key == "data")// these are the events
							{

								string ds = Newtonsoft.Json.JsonConvert.SerializeObject(k);
								string s = ds.Substring(22);
								s=s.Substring(0,s.Length-1);
								//string s = Newtonsoft.Json.JsonConvert.DSerializeObject(k);
								//string s = Convert.ToString(k.Value);

								SampleEvent  e = Newtonsoft.Json.JsonConvert.DeserializeObject<SampleEvent>(s);//<Dictionary<string,dynamic>>(s);


								//for(int i=0;i<o.Count;++i)//each(var d in o)
								//{
								string x="Event: " + e.ident + "~" + e.type + "~" + e.description;
								results.Add(x);
								System.Diagnostics.Debug.WriteLine(x.ToString());
								//}
							}

							//Neo4jEvent v=Newtonsoft.Json.JsonConvert.DeserializeObject<Neo4jEvent>(kvp);
							//System.Diagnostics.Debug.WriteLine( k.ToString());
						}
					}
				}

				/*
				RunOnUiThread (() =>
					{
						ListAdapter = new ArrayAdapter<string> (this, Resource.Layout.TweetItemView,results);
					});
				*/
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.Message);
			}
			list.ItemsSource = results;
			return results;
		}
	}
}

