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

using Google.YouTube;
using Google.GData;

/// <summary>
///  Calling the REST interface...
/// 
///  using System.Threading.Tasks;
///  Rest rest = new Rest ();
///  Task <List<string>> list = rest.SendAndReceiveJsonRequest ("http://71.92.131.203/db/data/cypher/","MATCH (a:Event) RETURN a LIMIT 25",lv);
///  ...in this case lv is a list view you want populated with 'Event' info
/// 
/// </summary>
// youtube key: AIzaSyDKynQvYpNj7gJncEFUIQhSlyB9xFfsU_g

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

								data.score = (string)token.SelectToken("score");
								try
								{
									data.toteScore = (int)token.SelectToken("toteScore");
									data.canScore = (int)token.SelectToken("canScore");
									data.auto = (string)token.SelectToken("auto");
									data.reliability = (string)token.SelectToken("reliability");
									data.reports=(int)token.SelectToken("reports");
									data.video=(string)token.SelectToken("video");
								}

								catch(Exception ex)
								{

								}
								if(data.video==null)
									data.video="https://www.youtube.com/watch?v=W6UYFKNGHJ8";//https://www.youtube.com/watch?v=-2NhcAbinDg
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

								try
								{
									data.timestamp=(double)token.SelectToken("timestamp");
									data.matchNumber = (string)token.SelectToken("matchNumber");
									data.notes = (string)token.SelectToken("notes");

									data.allianceScore = (string)token.SelectToken("score");
									data.canScore = (int)token.SelectToken("canScore");
									data.toteScore = (int)token.SelectToken("toteScore");
									data.brokeDown = (bool)token.SelectToken("brokeDown");
									data.autoCapability = (string)token.SelectToken("autoCapability");
									data.coopScore = (int)token.SelectToken("coopScore");
									data.noodleScore = (int)token.SelectToken("noodleScore");
								}
								catch(Exception ex)
								{
								}
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

		public async Task<string> saveVideo(int teamNum,string url)
		{
			string uri="http://71.92.131.203/db/data/cypher/";
			string query="MERGE (a:Team { number:"+teamNum.ToString()+" }) ON MATCH SET a.video=\""+url+"\" RETURN a";
			string responseStr = await SendAndReceiveJsonRequest(uri,query);
			return responseStr;
		}

		public async Task<string> queryYoutubeVideo(int teamNum)
		{

			//var settings = new YouTubeRequestSettings("BearBones", "AIzaSyDKynQvYpNj7gJncEFUIQhSlyB9xFfsU_g");
			//var request = new YouTubeRequest(settings);
			string query = "";
			string feedUrl = "";

			try
			{
				query=String.Format("search?part=snippet&q=\"FRC 2015 PNW {0}\"&maxResults=10&order=viewCount&key={1}",teamNum,"{AIzaSyDKynQvYpNj7gJncEFUIQhSlyB9xFfsU_g}");
				feedUrl = String.Format("https://www.googleapis.com/youtube/v3/{0}", query);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine (ex.Message);
			}
			string responseStr="";
			using (var httpClient = new HttpClient ())
			{        
				//create the http request content

				//HttpContent content = new StringContent(json);

				try {
					// Send the json to the server using POST

					Task<HttpResponseMessage> getResponse = httpClient.GetAsync (feedUrl);
					// Wait for the response and read it to a string var

					HttpResponseMessage response = await getResponse;
					responseStr = await response.Content.ReadAsStringAsync ();
				} catch (Exception exx) {
					System.Diagnostics.Debug.WriteLine (exx.Message);
				}
			}
			return responseStr;
			//var results = request.Get<Google.GData.YouTube.YouTubeService>(new Uri(feedUrl));
			//return "OK";
		}

		public async Task<string> createNewReport(ReportViewModel vm)
		{
			string uri="http://71.92.131.203/db/data/cypher/";
			string query;

			query = "CREATE (x:Report {timestamp:timestamp() , matchNumber:\"" + vm.matchNumber + "\"" +
					" ,score:\"" + vm.allianceScore + "\"" +
					" ,brokeDown:" + vm.brokeDown +
					" ,toteScore:\"" + vm.toteScore + "\" ,canScore:\"" + vm.canScore + "\"" +
					" ,coopScore:\"" + vm.coopScore + "\" ,noodleScore:\"" + vm.noodleScore + "\"" +
					" ,notes:\"" + vm.notes + "\" , autoCapability:\"" + vm.autoCapability + "\"" +
					"}) WITH x MATCH (a:Team) WHERE a.number = " + vm.teamNumber +
					" WITH x,a CREATE (a)-[r:HAS_REPORT]->(x)" +
				" WITH a SET  a.auto=\"" + vm.autoCapability + "\", a.reliability=\"" + vm.brokeDown + "\", a.score=\"" + vm.allianceScore + "\" ,a.toteScore=" + vm.toteScore + " ,a.canScore=" + vm.canScore + " ,a.coopScore=" + vm.coopScore + " ,a.noodleScore=" + vm.noodleScore+ ", a.reports=1 ";
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

