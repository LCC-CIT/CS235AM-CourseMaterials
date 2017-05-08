using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // holds planet name, relative gravity
            Dictionary<string, float> planets = 
                new Dictionary<string, float>();

            planets["Mercury"] = 0.5f;
            planets["Venus"] = 0.7f;
            planets["Earth"] = 1.0f;
            planets["Jupiter"] = 1.8f;

            Console.WriteLine("Gravity on Jupiter: "
                + planets["Jupiter"]);

            var people =
                new List<Dictionary<string, string>>();

            Dictionary<string, string> person =
                new Dictionary<string, string>();
            person["Name"] = "Rafael";
            person["Email"] = "rafael@gmail.com";
            person["Phone"] = "800-555-1212";
            people.Add(person);

            var person2 = new Dictionary<string, string>();
            person2["Name"] = "Michaelangelo";
            person2["Email"] = "mike@gmail.com";
            person2["Phone"] = "800-555-1213";
            people.Add(person2);

            var person3 = new Dictionary<string, string>();
            person3.Add("Name", "Leonardo");
            person3.Add("Email", "leo@gmail.com");
            person3.Add("Phone", "800-555-1214");
            people.Add(person3);

            foreach (Dictionary<string, string> d in people)
                Console.WriteLine(String.Format("{0}, {1}, {2}",
                    d["Name"], d["Email"], d["Phone"]));

        }
    }
}
