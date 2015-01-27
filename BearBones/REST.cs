using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using Microsoft.CSharp;
using ModernHttpClient;

using System.Linq;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;



/// <summary>
///  Calling the REST interface...
/// 
///  using System.Threading.Tasks;
///  Rest rest = new Rest ();
///  Task <List<string>> list = rest.SendAndReceiveJsonRequest ("http://71.92.131.203/db/data/cypher/","MATCH (a:Event) RETURN a LIMIT 25",lv);
///  ...in this case lv is a list view you want populated with 'Event' info
/// 
/// </summary>


namespace BearBones
{
	public class Rest
	{
		public Rest ()
		{
		}

		public async Task<ObservableCollection<InfoPageViewModel>> SendAndReceiveJsonRequest(string uri, string query,ObservableCollection<InfoPageViewModel> list)
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
			var results = new ObservableCollection<InfoPageViewModel>();
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


								string values = Newtonsoft.Json.JsonConvert.SerializeObject(k.Value);

								string ds = Newtonsoft.Json.JsonConvert.SerializeObject(k.Value);
								JToken token = JObject.Parse(ds);
								InfoPageViewModel data = new InfoPageViewModel();
								data.name = (string)token.SelectToken("name");
								data.number = (int)token.SelectToken("number");
								results.Add(data);
								//var v = Newtonsoft.Json.JsonConvert.DeserializeObject<InfoData>(values);
								//Dictionary<dynamic,dynamic> p = new Dictionary<dynamic, dynamic>{ { k.Key, k.Value } };

								//var pp = JObject.Parse(values);

								//foreach(var i in v.dict){
								
									/*string c = i.Key;

									switch(c){
									case "name":
										infoPage.name = i.Value;
										break;
									
									case "number":
										infoPage.number = i.Value;
										break;
									
									default:
										break;

									}
									*/

									/*if(i.Key = "name"){
										infoPage.name = i.Value;
									}else if(i.Key = "number"){
										infoPage.number = i.Value;
									}*/

								}
								//string ds = Newtonsoft.Json.JsonConvert.SerializeObject(k);
								//string s = ds.Substring(22);
								//s=s.Substring(0,s.Length-1);
								//string s = Newtonsoft.Json.JsonConvert.DSerializeObject(k);
								//string s = Convert.ToString(k.Value);



								//InfoPageViewModel e = Newtonsoft.Json.JsonConvert.DeserializeObject<InfoPageViewModel>(s);//<Dictionary<string,dynamic>>(s);


								//for(int i=0;i<o.Count;++i)//each(var d in o)
								//{



								//string x="Event: " + e.ident + "~" + e.type + "~" + e.description;
								//results.Add(infoPage);
								//System.Diagnostics.Debug.WriteLine(x.ToString());
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
			//}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.Message);
			}
			//.ItemsSource = results;
			return results;
		}
	}
}

