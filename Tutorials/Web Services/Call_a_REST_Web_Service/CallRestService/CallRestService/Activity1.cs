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
using System.Net;
using System.IO;
using System.Json;

namespace CallRestService
{
    [Activity (Label = "CallRestService", MainLauncher = true)]
    public class Activity1 : ListActivity
    {

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

			//string url =       "http://twitter.com/search.json?q=xamarin&rpp=10&include_entities=false&result_type=mixed";
			//															q=%23freebandnames&since_id=24012619984051000&max_id=250126199840518145&result_type=mixed&count=4
			string url = "https://api.twitter.com/1.1/search/tweets.json?q=xamarin&src=typd";
            var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
         
            httpReq.BeginGetResponse ((ar) => {
             
                var request = (HttpWebRequest)ar.AsyncState;
            
                using (var response = (HttpWebResponse)request.EndGetResponse (ar)) {
                 
                    var s = response.GetResponseStream ();
                 
                    var j = (JsonObject)JsonObject.Load (s);
            
                    var results = (from result in (JsonArray)j ["results"]
                                 let jResult = result as JsonObject
                                 select jResult ["text"].ToString ()).ToArray ();
            
                    RunOnUiThread (() => {
                        ListAdapter = new ArrayAdapter<string> (this, Resource.Layout.TweetItemView, results);
                    });
                }            

            }, httpReq);
        }

    }
}


