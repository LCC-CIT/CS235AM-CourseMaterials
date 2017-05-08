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
        /// <summary>
        /// The name of the flora
        /// </summary>
        /// <value>
        /// The name.
        /// </value>/
        public string Name { get; set; }

        /// <summary>
        /// Displays a formatted sub-total counts.
        /// </summary>
        /// <value>
        /// The item count.
        /// </value>
        public string ItemCount { get; set; }

        /// <summary>
        /// This holds a drawable resource id that will be displayed in an ImageView.
        /// </summary>
        /// <value>
        /// The image resource identifier.
        /// </value>
        public int ImageResourceId { get; set; }
    }
}