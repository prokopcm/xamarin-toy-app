using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace XamarinTestApp.Droid.Fragments
{
    public class HelloFragment : Fragment
    {
        private string Url = "http://jsonplaceholder.typicode.com/users/1";

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.HelloFragment, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            FetchWebData();
        }

        private async void FetchWebData()
        {
            var request = HttpWebRequest.Create(Url);
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        var respJson = JsonObject.Parse(content);
                        var name = (string) respJson["name"];

                        TextView apiResponse = (TextView)this.View.FindViewById(Resource.Id.apiResponse);
                        apiResponse.Text = name;
                    }
                }
            }
        }
    }
}