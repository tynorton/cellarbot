using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalWineParser.Model.Bevmo
{
    public class Beer
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brewery { get; set; }
        public bool IsInStock { get; set; }
        public string Price { get; set; }
        public Uri ThumbUri { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
