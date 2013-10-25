using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellarBotHome.Models
{
    public class SearchManager
    {
        private CellarBotEntities m_entities = new CellarBotEntities();

        public IEnumerable<sp_searchStyles_Result> GetStyleSearchResults(string term)
        {
            return this.m_entities.sp_searchStyles(term).OrderBy(obj => obj.style_name).ToList();
        }

        public IEnumerable<sp_searchBreweries_Result> GetBrewerySearchResults(string term)
        {
            return this.m_entities.sp_searchBreweries(term).OrderBy(obj => obj.name).ToList();
        }

        public IEnumerable<sp_searchBeers_Result> GetBeerSearchResults(string term)
        {
            return this.m_entities.sp_searchBeers(term).OrderBy(obj => obj.brewery_name + " - " + obj.beer_name).ToList();
        }
    }
}