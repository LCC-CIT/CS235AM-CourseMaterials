using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;

namespace EvolveLite
{
	/// <summary>
	/// Singleton :)
	/// </summary>
    public class SpeakersJsonParser
    {
        static readonly SpeakersJsonParser instance = new SpeakersJsonParser();

        List<Speaker> speakers;

        private SpeakersJsonParser()
        {
			SpeakersJsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Speakers.json");
        }

        public static SpeakersJsonParser Instance { get { return instance; } }

		public string SpeakersJsonFilePath { get; private set; }

		/// <summary>
		/// Lazy-load the speakers when they are first requested
		/// </summary>
        public List<Speaker> Speakers
        {
            get {
                if (speakers == null) {
                    speakers = GetSpeakers();
                }
                return speakers;
            }
        }

		/// <summary>
		/// Load the speakers from the file that was retrieved from the web
		/// </summary>
        List<Speaker> GetSpeakers()
        {
			var localSpeakers = new List<Speaker>();

			if (!File.Exists(SpeakersJsonFilePath)) {
				return new List<Speaker>();
			}

			using (var r = new StreamReader(File.OpenRead(SpeakersJsonFilePath))) {
                var j = (JsonArray)JsonValue.Load(r);

				foreach (var speaker in j) {
					var s = JsonToSpeaker (speaker);
					if (s != null)
						localSpeakers.Add (s);
				}
            }

			return localSpeakers;
        }


/*
{"name":"Nat Friedman",
"company":"Xamarin",
"position":"CEO",
"about":"Nat Friedman, Xamarin&rsquo;s co-founder and CEO, has been writing software for 27 years and is passionate about entrepreneurship and building amazing products. Nat will cover key mobile trends and predictions, Xamarin&rsquo;s future direction, and ways for you to capitalize on mobile - the largest software market ever.",
"url":"@natfriedman",
"avatar":"http://static.sched.org/a2/547192/avatar.jpg.75x75px.jpg"},
*/
        Speaker JsonToSpeaker(JsonValue json)
        {
            Speaker speaker = null;

            try {
				speaker = new Speaker();
				speaker.Name = json["name"];
				speaker.Bio = json["about"];
				speaker.Company = json["company"];
          	} catch {
				// lazy way to deal with missing json elements
			}
			Console.WriteLine (" Json'd: " + speaker.Name );
            return speaker;
        }
    }
}
