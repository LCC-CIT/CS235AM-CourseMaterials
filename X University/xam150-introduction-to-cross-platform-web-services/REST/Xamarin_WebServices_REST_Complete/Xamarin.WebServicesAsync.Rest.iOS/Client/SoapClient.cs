using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.WebServicesAsync.Soap.iOS.RxNav;

namespace Xamarin.WebServicesAsync.Soap.iOS.Client
{
	public class SoapClient
	{
		public SoapClient ()
		{
		}

		//TODO: 1 - create a SOAP client
		public async Task<IEnumerable<Model.ConceptProperty>> GetDataAsync(){
			return await Task.Run (() => {
				using(var soapClient = new DBManagerService ()){
					//Query for the drug named "aspirin"
					return SoapDtoToConceptProperty(soapClient.getDrugs("aspirin"));	
				}
			});
		}

		//TODO: 2 - Convert the SOAP DTOs into model objects
		private IEnumerable<Model.ConceptProperty> SoapDtoToConceptProperty(RxConceptGroup[] rxConceptGroups){
			var parsedConceptProperties = new List<Model.ConceptProperty>();

			foreach (var conceptGroup in rxConceptGroups) {
				foreach (var concept in conceptGroup.rxConcept) {
					parsedConceptProperties.Add(
						new Model.ConceptProperty(){
							Name = concept.STR,
							Synonym = concept.SY
						}
					);
				}
			}

			return parsedConceptProperties.OrderBy(cp => cp.Synonym);
		}
	}
}

