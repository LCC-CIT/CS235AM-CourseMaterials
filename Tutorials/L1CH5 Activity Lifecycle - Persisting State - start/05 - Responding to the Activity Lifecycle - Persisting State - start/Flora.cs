namespace PersistStateWalkthrough
{
    /// <summary>
    ///   This class is for objects that will hold information about what
    ///   data to display in each row.
    /// </summary>
    public class Flora
    {
        /// <summary>
        ///   This holds the resource id for a drawable that will be displayed in an ImageView.
        /// </summary>
        /// <value> The image resource identifier. </value>
        public int ImageResourceId { get; set; }
        public string ItemCount { get; set; }
        public string Name { get; set; }
    }
}
