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
        public static void SaveObject(Object obj)
        {
            string str = JsonConvert.SerializeObject(obj, Formatting.None);
            SaveString(str);
        }

        public static T GetSavedObject<T>()
        {
            string str = GetSavedString();
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static void SetDebug()
        {
            Url = "https://api.myjson.com/bins/pmhrp";
        }

        public static void SetRelease()
        {
            Url = "https://api.myjson.com/bins/88f6t";
        }

        private static String Url { get; set; }

        //Private stuff dont look

        private static void SaveString(string value)
        {
            using (var client = new HttpClient())
            {
                var values = new KeyValuePair<string, string>("Json", value);
                string jsonString = JsonConvert.SerializeObject(values, Formatting.None);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var message = client.PutAsync("https://api.myjson.com/bins/88f6t", httpContent);

                var test = message.Result;
            }
        }

        private static string GetSavedString()
        {
            var url = "https://api.myjson.com/bins/88f6t";

            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return JsonConvert.DeserializeObject<KeyValuePair<string, string>>(responseString).Value;
        }
    }
}
