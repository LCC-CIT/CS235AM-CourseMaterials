using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Xamarin.WebServicesAsync.Rest.Droid.Client
{
	public class RestClient
	{


		//TODO: Step 1 - Create a REST client
//		private const string 
//			RestServiceBaseAddress = "http://rxnav.nlm.nih.gov/REST/",
//			AcceptHeaderApplicationJson = "application/json";
//
//		private HttpClient CreateRestClient(){
//			var client = new HttpClient(){ BaseAddress = new Uri(RestServiceBaseAddress) };
//
//			client.DefaultRequestHeaders.Accept.Add (MediaTypeWithQualityHeaderValue.Parse(AcceptHeaderApplicationJson));
//
//			return client;
//		} 

		//TODO: Step 2 - Call the web service and retrieve data
//		public async Task<IEnumerable<Model.ConceptProperty>> GetDataAsync(){
//			using (var client = CreateRestClient ()) {
//				var getDataResponse = await client.GetAsync ("drugs?name=aspirin", HttpCompletionOption.ResponseContentRead);
//
//				//If we do not get a successful status code, then return an empty set
//				if (!getDataResponse.IsSuccessStatusCode)
//					return Enumerable.Empty<Model.ConceptProperty> ();
//
//				//Stream the response into a JSON Value object
//				var jsonResponse = JsonValue.Load(await getDataResponse.Content.ReadAsStreamAsync ());
//
//						//TODO: Step 5 - call to map JSON to model object
////						return MapJsonToConceptProperty (jsonResponse);
//			}
//		}

		//TODO: Step 3 - Convert JSON into model objects
//		private IEnumerable<Model.ConceptProperty> MapJsonToConceptProperty(JsonValue json)
//		{
//			var conceptProperties = new List<Model.ConceptProperty>();
//
//			if(json != null && json.ContainsKey("drugGroup")){
//				var drugGroup = json ["drugGroup"];
//
//				if (drugGroup != null && drugGroup.ContainsKey("conceptGroup")){
//					foreach (JsonValue conceptGroup in drugGroup["conceptGroup"]) {
//						if(conceptGroup.ContainsKey("conceptProperties")){
//							foreach (JsonValue conceptProperty in conceptGroup["conceptProperties"]) {
//								conceptProperties.Add(
//									new Model.ConceptProperty(){
//										//TODO: Step 4 - Map the properties from our DTO to custom object
//									});
//							}
//						}
//					}
//				}
//			}
//
//			return conceptProperties.OrderBy(cp => cp.Synonym);
//		}
	}
}

