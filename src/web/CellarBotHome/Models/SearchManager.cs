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
            return this.m_entities.sp_searchStyles(term).ToList();
        }

        public IEnumerable<sp_searchBreweries_Result> GetBrewerySearchResults(string term)
        {
            return this.m_entities.sp_searchBreweries(term).ToList();
        }

        public IEnumerable<sp_searchBeers_Result> GetBeerSearchResults(string term)
        {
            return this.m_entities.sp_searchBeers(term).ToList();
        }
    }
}