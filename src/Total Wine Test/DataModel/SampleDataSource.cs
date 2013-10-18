using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TotalWineParser.DataModel.TotalWine;
using TotalWineParser.Model.Bevmo;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace Total_Wine_Test.Data
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem
    {
        public SampleDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Content = content;
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup
    {
        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Items = new ObservableCollection<SampleDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<SampleDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<SampleDataItem> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetSampleDataAsync()
        {
            Uri uri = new Uri("http://www.microsoft.com");
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler);

            var result = await client.GetStringAsync(new Uri("https://m.totalwine.com/json/product/list?id=%2Feng%2Fcategories%2Fbeer"));

            var obj = JsonConvert.DeserializeObject<RootObject>(result);


            SampleDataGroup group = new SampleDataGroup(Guid.NewGuid().ToString(),
                                                        "Beer",
                                                        "All Total Wine Beers",
                                                        string.Empty,
                                                        "There are beers in here");

            foreach (Product product in obj.resultset.Products)
            {
                group.Items.Add(new SampleDataItem(product.ProductId,
                                                   product.Name,
                                                   product.SubHeader,
                                                   product.Image.SmallUrl,
                                                   product.Description,
                                                   string.Empty));
            }

            this.Groups.Add(group);

            // throws "The server committed a protocol violation. Section=ResponseHeader Detail=CR must be followed by LF"
            // cannot bypass using HttpClient, need 3rd party library
            /*var loginRes = await client.GetStringAsync(new Uri("http://m.bevmo.com/Misc/FulfillmentSelectDropDown.aspx?fullfilmentMethod=1&fullfilmentLocation=171&_=" + GetEpocTime() + "000"));

            var beers = new List<Beer>();
            var resultHtml = await client.GetStringAsync(new Uri("http://m.bevmo.com/Shop/ProductList.aspx/Beer/Craft-Brew/_/N-15Z1z13tvr?DNID=Beer"));

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(resultHtml); // or Load
            var desc = doc.DocumentNode.Descendants();
            var nodes = desc.Where(node => node.Attributes["class"].Value.Contains("ProductListItemTable"));
            foreach (var node in nodes)
            {
                var productName = node.Descendants().Where(n => n.Attributes["id"].Value.Contains("uxProductLink")).FirstOrDefault().InnerText;
                var imgUri = node.Descendants().Where(n => n.Attributes["id"].Value.Contains("uxProductIconImage")).FirstOrDefault().Attributes["data-src"].Value.Replace("../../../../..", "http://m.bevmo.com");

                beers.Add(new Beer { Name = productName, ThumbUri = new Uri(imgUri) });
            }*/
        }

        private static string GetEpocTime()
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            TimeSpan span = (DateTime.UtcNow.ToLocalTime() - epoch);
            return span.TotalSeconds.ToString().Split('.')[0];
        }
    }
}