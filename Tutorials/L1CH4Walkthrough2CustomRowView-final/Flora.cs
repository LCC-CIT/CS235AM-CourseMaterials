using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CustomRowView
{
    /// <summary>
    /// This class is for objects that will hold information about what
    /// data to display in each row.
    /// </summary> 
    public class Flora
    {
        public string Name { get; set; }

        public string ItemCount { get; set; }

        /// <summary>
        /// This holds the resource id for a drawable that will be displayed in an ImageView.
        /// </summary>
        /// <value>
        /// The image resource identifier.
        /// </value>
        public int ImageResourceId { get; set; }
    }
}