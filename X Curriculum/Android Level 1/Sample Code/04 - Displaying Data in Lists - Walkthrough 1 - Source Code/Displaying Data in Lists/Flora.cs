namespace BasicTable
{
    /// <summary>
    ///   A simple class for holding the data that is read from VegeData.txt
    /// </summary>
    public class Flora
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
