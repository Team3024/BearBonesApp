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

		public async Task<ObservableCollection<HomePageViewModel>> getAllTeams()
		{
			string uri = "http://71.92.131.203/db/data/cypher/";
			string query = "MATCH (a:Team) RETURN a";
			string responseStr = await SendAndReceiveJsonRequest(uri,query);

			var results = new ObservableCollection<HomePageViewModel>();

			try
			{
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
								HomePageViewModel data = new HomePageViewModel(typeof(HomePage));
								data.teamName = (string)token.SelectToken("name");

								if (token.SelectToken("number")!=null)
									data.teamNumber=(int)token.SelectToken("number");
								else
									data.teamNumber=-1;

								results.Add(data);
							}
						}
					}
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.Message);
			}

			return results;
		}

		public async Task<ObservableCollection<InfoPageViewModel>>getReports(int teamNum)
		{
			string uri="http://71.92.131.203/db/data/cypher/";
			string query="MATCH (a:Team)-[HAS_REPORT]->(b:Report) where a.number="+teamNum.ToString()+" RETURN b";
			string responseStr = await SendAndReceiveJsonRequest(uri,query);
		
			var results = new ObservableCollection<InfoPageViewModel>();

			try
			{
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
								data.scout = (string)token.SelectToken("scout");
								data.drive = (string)token.SelectToken("drive");
								if (token.SelectToken("score")!=null)
									data.score =(int)token.SelectToken("score");
								else
									data.score=-1;
								results.Add(data);
							}
						}
					}
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.Message);
			}

			return results;
		}

		public async Task<string> createNewTeam(int teamNum,string teamName,string scout)
		{
			string uri="http://71.92.131.203/db/data/cypher/";
			string query="MERGE (a:Team { number:"+teamNum.ToString()+" }) ON CREATE SET a.name=\""+teamName+"\" , a.scout=\""+scout+"\" RETURN a";
			string responseStr = await SendAndReceiveJsonRequest(uri,query);
			return responseStr;
		}

		public async Task<string> createNewReport(ReportViewModel vm)
		{
			string uri="http://71.92.131.203/db/data/cypher/";
			string query="CREATE (x:Report {type:\"" + vm.reportType + "\", scout:\"" + vm.scoutName + "\",driveType:\"" + vm.driveType + "\" ,score:\"" + vm.score + "\"}) WITH x MATCH (a:Team) WHERE a.number = \"" + vm.teamNumber + "\" WITH x,a CREATE (a)-[r:HAS_REPORT]->(x)";
			string responseStr = await SendAndReceiveJsonRequest(uri,query);
			return responseStr;
		}

		public async Task<string> SendAndReceiveJsonRequest(string uri, string query)
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
			return responseStr;
		}
	}
}

