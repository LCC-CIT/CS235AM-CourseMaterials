using System;
using System.Collections.Generic;
using System.IO;

namespace DataAccess.DOS
{
    /// <summary>
    /// Contains methods for parsing csd text files
    /// </summary>
    public class TextParser
    {
        string delimiter;
        int numFields;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAndParser.TextParser"/> class.
        /// </summary>
        /// <param name="columnDelimiter">String that separates columns in the text being parsed. Examples: ",", "\t", " ".</param>
        /// <param name="numberOfFields">Number of columns in the text being parsed.</param>
        public TextParser(string columnDelimiter, int numberOfFields)
        {
            delimiter = columnDelimiter;
            numFields = numberOfFields;
        }

        public List<string[]> ParseText(Stream stream)
        {
            List<string[]> rows = new List<string[]>();
            /* For testing
			rows.Add (new string[] {"mono", "monkey", "noun"});
			rows.Add (new string[] {"agua", "water", "noun"});
			rows.Add (new string[] {"si", "yes", "noun"});
			rows.Add (new string[] {"perro", "dog", "noun"});
			rows.Add (new string[] {"gato", "cat", "noun" });
			rows.Add (new string[] {"sustantivo", "noun", "noun" });
			*/

            string[] delim = new string[1];
            delim[0] = delimiter;

            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    rows.Add(reader.ReadLine().Split(delim, numFields, StringSplitOptions.None));
                }
            }

            return rows;
        }
    }
}