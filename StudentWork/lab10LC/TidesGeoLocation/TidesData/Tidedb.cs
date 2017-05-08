using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Xml.Serialization;



namespace TideFragments.DAL
{
	[Table("Location")]
	public class Location
	{
		[PrimaryKey,AutoIncrement]	
		public int Id{ get; set; }
		public string LocationId{ get; set ; }
		public string LocationName{ get; set; }
		public string Latitude {get; set; }
		public string Longitude {get; set; }

	}

	[Table("TideTable")]
	public class TideTable
	{
		[PrimaryKey,AutoIncrement]
		public int TideId{get; set; }
		public string  LocationId{get;set;}
		public string Date{get; set; }
		public string Day{get; set;}
		public string Time{ get; set; }
		public string PredictedFt{ get; set; }
		public string PredictedCm{ get; set;}
		public string HighLow{get; set;}
		//public TideItem[] listField;

	} 


	class TideTableComparer : IEqualityComparer<TideTable>
	{
		// Tides are equal if their Dates are equal. 
		public bool Equals(TideTable x, TideTable y)
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

		public int GetHashCode(TideTable tide)
		{
			//Check whether the object is null 
			if (Object.ReferenceEquals(tide, null)) return 0;

			//Get hash code for the Date field if it is not null. 
			int hashTideDate = tide.Date == null ? 0 : tide.Date.GetHashCode();

			//Calculate the hash code for the tide. 
			return hashTideDate;
		}
	}

	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName="datainfo")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]

	    public partial class Datainfo
		{

			private string originField;

			private string disclaimerField;

			private string producttypeField;

			private string stationnameField;

			private string stateField;

			private uint stationidField;

			private string stationtypeField;
		private string referencedToStationNameField;

		private uint referencedToStationIdField;

		private string heightOffsetLowField;

		private string heightOffsetHighField;

		private byte timeOffsetLowField;

		private byte timeOffsetHighField;


			private string beginDateField;

			private string endDateField;

			private string dataUnitsField;

			private string timezoneField;

			private string datumField;

			private string intervalTypeField;

		   private DataInfoItem[] dataField;

			/// <remarks/>
			public string origin
			{
				get
				{
					return this.originField;
				}
				set
				{
					this.originField = value;
				}
			}

			/// <remarks/>
			public string disclaimer
			{
				get
				{
					return this.disclaimerField;
				}
				set
				{
					this.disclaimerField = value;
				}
			}

			/// <remarks/>
			public string producttype
			{
				get
				{
					return this.producttypeField;
				}
				set
				{
					this.producttypeField = value;
				}
			}

			/// <remarks/>
			public string stationname
			{
				get
				{
					return this.stationnameField;
				}
				set
				{
					this.stationnameField = value;
				}
			}

			/// <remarks/>
			public string state
			{
				get
				{
					return this.stateField;
				}
				set
				{
					this.stateField = value;
				}
			}

			/// <remarks/>
			public uint stationid
			{
				get
				{
					return this.stationidField;
				}
				set
				{
					this.stationidField = value;
				}
			}

			/// <remarks/>
			public string stationtype
			{
				get
				{
					return this.stationtypeField;
				}
				set
				{
					this.stationtypeField = value;
				}
			}
		/// <remarks/>
		public string referencedToStationName {
			get {
				return this.referencedToStationNameField;
			}
			set {
				this.referencedToStationNameField = value;
			}
		}

		/// <remarks/>
		public uint referencedToStationId {
			get {
				return this.referencedToStationIdField;
			}
			set {
				this.referencedToStationIdField = value;
			}
		}

		/// <remarks/>
		public string HeightOffsetLow {
			get {
				return this.heightOffsetLowField;
			}
			set {
				this.heightOffsetLowField = value;
			}
		}

		/// <remarks/>
		public string HeightOffsetHigh {
			get {
				return this.heightOffsetHighField;
			}
			set {
				this.heightOffsetHighField = value;
			}
		}

		/// <remarks/>
		public byte TimeOffsetLow {
			get {
				return this.timeOffsetLowField;
			}
			set {
				this.timeOffsetLowField = value;
			}
		}

		/// <remarks/>
		public byte TimeOffsetHigh {
			get {
				return this.timeOffsetHighField;
			}
			set {
				this.timeOffsetHighField = value;
			}
		}


			/// <remarks/>
			public string BeginDate
			{
				get
				{
					return this.beginDateField;
				}
				set
				{
					this.beginDateField = value;
				}
			}

			/// <remarks/>
			public string EndDate
			{
				get
				{
					return this.endDateField;
				}
				set
				{
					this.endDateField = value;
				}
			}

			/// <remarks/>
			public string dataUnits
			{
				get
				{
					return this.dataUnitsField;
				}
				set
				{
					this.dataUnitsField = value;
				}
			}

			/// <remarks/>
			public string Timezone
			{
				get
				{
					return this.timezoneField;
				}
				set
				{
					this.timezoneField = value;
				}
			}

			/// <remarks/>
			public string Datum
			{
				get
				{
					return this.datumField;
				}
				set
				{
					this.datumField = value;
				}
			}

			/// <remarks/>
			public string IntervalType
			{
				get
				{
					return this.intervalTypeField;
				}
				set
				{
					this.intervalTypeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
		public DataInfoItem[] data
			{
				get
				{
					return this.dataField;
				}
				set
				{
					this.dataField = value;
				}
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class DataInfoItem
		{

			private string dateField;

			private string dayField;

			private string timeField;

			private decimal predictions_in_ftField;

			private short predictions_in_cmField;

			private string highlowField;

			/// <remarks/>
			public string date
			{
				get
				{
					return this.dateField;
				}
				set
				{
					this.dateField = value;
				}
			}

			/// <remarks/>
			public string day
			{
				get
				{
					return this.dayField;
				}
				set
				{
					this.dayField = value;
				}
			}

			/// <remarks/>
			public string time
			{
				get
				{
					return this.timeField;
				}
				set
				{
					this.timeField = value;
				}
			}

			/// <remarks/>
			public decimal predictions_in_ft
			{
				get
				{
					return this.predictions_in_ftField;
				}
				set
				{
					this.predictions_in_ftField = value;
				}
			}

			/// <remarks/>
			public short predictions_in_cm
			{
				get
				{
					return this.predictions_in_cmField;
				}
				set
				{
					this.predictions_in_cmField = value;
				}
			}

			/// <remarks/>
			public string highlow
			{
				get
				{
					return this.highlowField;
				}
				set
				{
					this.highlowField = value;
				}
			}
		}


}



