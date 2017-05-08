using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.WebServicesAsync.Soap.Droid.RxNav;

namespace Xamarin.WebServicesAsync.Soap.Droid.Client
{
	public class SoapClient
	{
		public SoapClient ()
		{
		}

		//TODO: Step 1 - create a SOAP client
//		public async Task<IEnumerable<Model.ConceptProperty>> GetDataAsync(){
//			return await Task.Run (() => {
//				using(var soapClient = new DBManagerService ()){
//					//TODO: Step 3 - map the response from the web service.
//					//Query for the drug named "aspirin"
//					var foundMatches = soapClient.getDrugs("aspirin");
//				   	return MapSoapDtoToConceptProperty(foundMatches);	
//				}
//			});
//		}

		//TODO: Step 2 - Convert the SOAP DTOs into model objects
//		private IEnumerable<Model.ConceptProperty> MapSoapDtoToConceptProperty(RxConceptGroup[] rxConceptGroups){
//			var parsedConceptProperties = new List<Model.ConceptProperty>();
//
//			foreach (var conceptGroup in rxConceptGroups) {
//				foreach (var concept in conceptGroup.rxConcept) {
//					parsedConceptProperties.Add(
//						new Model.ConceptProperty(){
//						}
//					);
//				}
//			}
//
//			return parsedConceptProperties.OrderBy(cp => cp.Synonym);
//		}
	}
}

