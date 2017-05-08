using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;

namespace TheBestAppEver
{
	public class WebService
	{
		static WebService shared;
		public static WebService Shared {
			get {
				return shared ?? (shared = new WebService());
			}
		}

		public async Task<List<Song>> GetSongs()
		{
			var client = new WebClient ();
			//Smaller json file (1.2mb)
			const string smallFile = "https://dl.dropboxusercontent.com/s/cv75h76pv9su7l4/songs-small.json";
			const string bigFile = "https://dl.dropboxusercontent.com/s/wl3rsarzf52pcf6/songs.json";

			var data = await client.DownloadStringTaskAsync (smallFile);
			return await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(data));
		}

		public async Task<List<TodoItem>> GetItems()
		{
			var client = new WebClient ();
			var data = await client.DownloadStringTaskAsync ("http://bucketlist.org/api/list/shacker/all/");
			return await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<List<TodoItem>>(data));
		}
	}
}

