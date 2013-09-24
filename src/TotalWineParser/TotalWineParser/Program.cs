using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TotalWineParser.Model.Bevmo;
using TotalWineParser.Model.TotalWine;

namespace TotalWineParser
{
    class Program
    {
        public const string IOS_USER_AGENT = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_2 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8H7 Safari/6533.18.5";

        static CookieContainer s_cookieContainer = new CookieContainer();
        static void Main(string[] args)
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = s_cookieContainer,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("user-agent", IOS_USER_AGENT);

            var result = client.GetStringAsync("https://m.totalwine.com/json/product/list?id=%2Feng%2Fcategories%2Fbeer").Result;

            var obj = JsonConvert.DeserializeObject<RootObject>(result);
            foreach (var beer in obj.resultset.Products)
            {
                Console.WriteLine(beer.Name);
            }

            // Bellevue Store
            //fullfilmentMethod	1
            //fullfilmentLocation	171
            client.GetAsync("http://m.bevmo.com/Misc/FulfillmentSelectDropDown.aspx?fullfilmentMethod=1&fullfilmentLocation=171&_=" + GetEpocTime() + "000");

            var beers = new List<Beer>();
            var resultHtml = client.GetStringAsync("http://m.bevmo.com/Shop/ProductList.aspx/Beer/Craft-Brew/_/N-15Z1z13tvr?DNID=Beer").Result;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(resultHtml); // or Load
            var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'ProductListItemTable')]");
            foreach (var node in nodes)
            {
                var productName = node.SelectSingleNode(".//a[contains(@id, 'uxProductLink')]").InnerText;
                var imgUri = node.SelectSingleNode(".//img[contains(@id, 'uxProductIconImage')]").Attributes["data-src"].Value.Replace("../../../../..", "http://m.bevmo.com");

                beers.Add(new Beer { Name = productName, ThumbUri = new Uri(imgUri) });
                Console.WriteLine(productName);
            }

            Console.Read();
        }


        private static string GetEpocTime()
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            TimeSpan span = (DateTime.UtcNow.ToLocalTime() - epoch);
            return span.TotalSeconds.ToString().Split('.')[0];
        }
    }
}
