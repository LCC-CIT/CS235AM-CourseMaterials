using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TideFragments
{
	public class Tide
	{
		public string Date{ get; set; }
		public string Day{ get; set; }
		public string Time{ get; set; }
		public string PredFt{ get; set; }
		public string PredCm{ get; set; }
		public string HighLow {get; set; }
		List<Tide> tempTideList = new List<Tide> ();
		List<Tide> myList = new List<Tide> ();
		List<string>data=new List<string>();


		public Tide(string d, string y, string t, string ft, string cm, string hl)
		{
			Date = d;
			Day = y;
			Time = t;
			PredFt = ft;
			PredCm = cm;
			HighLow = hl;
		}

	






	}



	// Custom comparer for the Tide class 
	class TideComparer : IEqualityComparer<Tide>
	{
		// Tides are equal if their Dates are equal. 
		public bool Equals(Tide x, Tide y)
		{

			//Check whether the compared objects reference the same data. 
			if (Object.ReferenceEquals(x, y)) return true;

			//Check whether any of the compared objects is null. 
			if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
				return false;

			//Check whether the Dates properties are equal. 
			return x.Date == y.Date;
		}

		// If Equals() returns true for a pair of objects  
		// then GetHashCode() must return the same value for these objects. 

		public int GetHashCode(Tide tide)
		{
			//Check whether the object is null 
			if (Object.ReferenceEquals(tide, null)) return 0;

			//Get hash code for the Date field if it is not null. 
			int hashTideDate = tide.Date == null ? 0 : tide.Date.GetHashCode();

			//Calculate the hash code for the tide. 
			return hashTideDate;
		}

	}


}

