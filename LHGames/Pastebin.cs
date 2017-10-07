using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


/*
Example usage:

1. Set pastebin to debug or release

LHGames.Pastebin.SetDebug();



2. Create a class with public properties

public class Test
{
    public String str { get; set; }
    public int i { get; set; }
}


3. Push to server

Test t = new Test();
t.str = "my str test";
t.i = 42;
LHGames.Pastebin.SaveObject(t);


4. Get from server

Test newT = LHGames.Pastebin.GetSavedObject<Test>();
*/



namespace LHGames
{
    static public class Pastebin
    {
        public static readonly string MY_PLAYER_STATS_URL = "https://api.myjson.com/bins/pmhrp";
        public static readonly string DEBUG_LOG_CHROUS_URL = "https://api.myjson.com/bins/vroqd";

        public static void SaveObject(string url, Object obj)
        {
            string str = JsonConvert.SerializeObject(obj, Formatting.None);
            SaveString(url, str);
        }

        public static T GetSavedObject<T>(string url)
        {
            string str = GetSavedString(url);
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static void AppendString(string url, string value)
        {
            string mystr = GetSavedString(url);
            mystr += value;
            SaveString(url, mystr);
        }

        public static void SaveString(string url, string value)
        {
            using (var client = new HttpClient())
            {
                var values = new KeyValuePair<string, string>("Json", value);
                string jsonString = JsonConvert.SerializeObject(values, Formatting.None);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var message = client.PutAsync(url, httpContent);

                var test = message.Result;
            }
        }

        public static string GetSavedString(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return JsonConvert.DeserializeObject<KeyValuePair<string, string>>(responseString).Value;
        }
    }
}
