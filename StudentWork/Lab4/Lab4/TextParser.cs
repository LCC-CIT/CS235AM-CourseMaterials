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
using System.IO;

namespace Lab2
{
	public class TextParser
	{
		public string delimiter;
		public int numFields;

		public TextParser(string d, int nf) 
		{ 
			delimiter = d;
			numFields = nf;
	 	}

		public List<string[]> ParseText(Stream stream)
		{
			List<string[]> rows = new List<string[]> ();

			string[] delim = new string[1];
			delim [0] = delimiter;

			using (var reader = new StreamReader(stream)) 
			{
				while(!reader.EndOfStream)
					rows.Add(reader.ReadLine().Split(delim, numFields, StringSplitOptions.None));
			}

			return rows;
		}
	}
}

