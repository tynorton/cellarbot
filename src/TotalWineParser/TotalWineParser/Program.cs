using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TotalWineParser.Model.TotalWine;

namespace TotalWineParser
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            var result = client.GetStringAsync("https://m.totalwine.com/json/product/list?id=%2Feng%2Fcategories%2Fbeer").Result;

            var obj = JsonConvert.DeserializeObject<RootObject>(result);


            JsonTextReader reader = new JsonTextReader(new StringReader(result));
            while (reader.Read())
            {
                if (reader.Value != null)
                    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                else
                    Console.WriteLine("Token: {0}", reader.TokenType);
            }

            Console.Read();
        }
    }
}
