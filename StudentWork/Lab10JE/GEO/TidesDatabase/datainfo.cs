using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using SQLite;

namespace Preloader
{

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute( AnonymousType = true )]
	[System.Xml.Serialization.XmlRootAttribute( Namespace = "", IsNullable = false, ElementName = "datainfo" )]
	public partial class datainfo
	{
		public string originField;

		public string disclaimerField;

		public string producttypeField;

		public string stationnameField;

		public string stateField;

		public uint stationidField;

		public string stationtypeField;

		public string referencedToStationNameField;

		public uint referencedToStationIdField;

		public string heightOffsetLowField;

		public string heightOffsetHighField;

		public byte timeOffsetLowField;

		public byte timeOffsetHighField;

		public string beginDateField;

		public string endDateField;

		public string dataUnitsField;

		public string timezoneField;

		public string datumField;

		public string intervalTypeField;

		public datainfoItem[] dataField;

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
		public string referencedToStationName
		{
			get
			{
				return this.referencedToStationNameField;
			}
			set
			{
				this.referencedToStationNameField = value;
			}
		}

		/// <remarks/>
		public uint referencedToStationId
		{
			get
			{
				return this.referencedToStationIdField;
			}
			set
			{
				this.referencedToStationIdField = value;
			}
		}

		/// <remarks/>
		public string HeightOffsetLow
		{
			get
			{
				return this.heightOffsetLowField;
			}
			set
			{
				this.heightOffsetLowField = value;
			}
		}

		/// <remarks/>
		public string HeightOffsetHigh
		{
			get
			{
				return this.heightOffsetHighField;
			}
			set
			{
				this.heightOffsetHighField = value;
			}
		}

		/// <remarks/>
		public byte TimeOffsetLow
		{
			get
			{
				return this.timeOffsetLowField;
			}
			set
			{
				this.timeOffsetLowField = value;
			}
		}

		/// <remarks/>
		public byte TimeOffsetHigh
		{
			get
			{
				return this.timeOffsetHighField;
			}
			set
			{
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
		[System.Xml.Serialization.XmlArrayItemAttribute( "item", IsNullable = false )]
		public datainfoItem [] data
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
	[System.Xml.Serialization.XmlTypeAttribute( AnonymousType = true )]
	public partial class datainfoItem
	{

		public string dateField;

		public string dayField;

		public string timeField;

		public decimal predictions_in_ftField;

		public short predictions_in_cmField;

		public string highlowField;

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


