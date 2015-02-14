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

		public async Task<ObservableCollection<ReportViewModel>>getReports(int teamNum)
		{
			string uri="http://71.92.131.203/db/data/cypher/";
			string query="MATCH (a:Team)-[HAS_REPORT]->(b:Report) where a.number="+teamNum.ToString()+" RETURN b";
			string responseStr = await SendAndReceiveJsonRequest(uri,query);
		
			var results = new ObservableCollection<ReportViewModel>();

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
								ReportViewModel data = new ReportViewModel();
								data.reportType = (string)token.SelectToken("type");
								data.scoutName = (string)token.SelectToken("scout");
								data.allianceScore = (string)token.SelectToken("score");
								data.canScore = (string)token.SelectToken("canScore");
								data.toteScore = (string)token.SelectToken("toteScore");
								data.driveType = (string)token.SelectToken("driveType");
								data.matchNumber = (string)token.SelectToken("matchNumber");
								data.grabsContainer = (bool)token.SelectToken("grabsContainer");

								data.maxStack = (string)token.SelectToken("maxStack");
								data.grabsTote = (bool)token.SelectToken("grabsTote");
								data.lastYearFinish = (string)token.SelectToken("lastYearFinish");
								data.grabsToteOffStep = (bool)token.SelectToken("grabsToteOffStep");
								data.noodleBonus = (bool)token.SelectToken("noodleBonus");
								data.notes = (string)token.SelectToken("notes");

								data.noodleCleanup = (bool)token.SelectToken("noodleCleanup");
								data.brokeDown = (bool)token.SelectToken("brokeDown");
								data.grabsContainerOffStep = (bool)token.SelectToken("grabsContainerOffStep");
								data.noodleInContainer = (bool)token.SelectToken("noodleInContainer");
								data.teamQuality = (string)token.SelectToken("teamQuality");
								data.autoCapability = (string)token.SelectToken("autoCapability");
								/*
								if (token.SelectToken("score")!=null)
									data.score =(int)token.SelectToken("score");
								else
									data.score=-1;
									*/
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
			string query="CREATE (x:Report {type:\"" + vm.reportType + "\",matchNumber:\""+vm.matchNumber+"\""+
				" ,scout:\"" + vm.scoutName + "\",driveType:\"" + vm.driveType + "\" ,score:\"" + vm.allianceScore + "\""+
				" ,maxStack:\"" + vm.maxStack + "\",grabsContainer:" + vm.grabsContainer + " ,grabsTote:" + vm.grabsTote +
				" ,grabsContainerOffStep:" + vm.grabsContainerOffStep + ",grabsToteOffStep:" + vm.grabsToteOffStep +
				" ,brokeDown:" + vm.brokeDown + " ,lastYearFinish:\"" + vm.lastYearFinish+"\""+
				" ,toteScore:" + vm.toteScore + " ,canScore:\"" + vm.canScore+"\""+
				" ,noodleBonus:" + vm.noodleBonus + " ,noodleCleanup:" + vm.noodleCleanup +" ,noodleInContainer:" + vm.noodleInContainer +
				" ,notes:\"" + vm.notes +"\" ,teamQuality:\"" + vm.teamQuality +"\" ,autoCapability:\"" + vm.autoCapability +"\""+
				"}) WITH x MATCH (a:Team) WHERE a.number = " + vm.teamNumber +
				" WITH x,a CREATE (a)-[r:HAS_REPORT]->(x)";
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

