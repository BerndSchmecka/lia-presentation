using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace sample_code
{
    class Program
    {
        static string API_URL = "https://api.quotable.io/random";

        static void Main(string[] args)
        {
            Console.Write("Enter tag(s), leave empty for none: ");
            string buffer = Console.ReadLine();

            buffer = buffer.Replace(" ", "");

            if(buffer.Length > 0) API_URL = String.Concat(API_URL, $"?tags={buffer}");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(API_URL);
            request.Method = "GET";

            try{
                using(HttpWebResponse response = (HttpWebResponse)request.GetResponse()){
                    using(StreamReader sr = new StreamReader(response.GetResponseStream())){
                        string jsonResponse = sr.ReadToEnd();
                        dynamic quote = JsonConvert.DeserializeObject(jsonResponse);
                        Console.WriteLine($"Received Quote: \"{quote.content}\" - {quote.author}");
                        Console.WriteLine($"Tags: {String.Join(", ",quote.tags)}");
                  }
                }
            } catch (WebException e) {
                var response = (HttpWebResponse)e.Response;
                using(StreamReader sr = new StreamReader(response.GetResponseStream())){
                    if((int)response.StatusCode == 404){
                        string errorResponse = sr.ReadToEnd();
                        dynamic error = JsonConvert.DeserializeObject(errorResponse);
                        Console.WriteLine($"Error getting quote: {error.statusMessage}");
                    } else {
                        string errorResponse = sr.ReadToEnd();
                        Console.WriteLine($"Error {response.StatusCode} returned: {errorResponse}");
                    }
                }
            }
        }
    }
}
