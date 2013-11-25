using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CellarBotHome.Models
{
    public static class CellarExtensions
    {
        public static IEnumerable<Cellar> GetUserCellars(this CellarBotEntities ents, string userId)
        {
            return (from c in ents.Cellars
                           where c.UserID == userId
                           select c)
                .OrderBy(obj => obj.Name)
                .ToList();
        }

        public static Cellar GetCellar(this CellarBotEntities ents, int cellarId)
        {
            return (from c in ents.Cellars
                    where c.ID == cellarId
                    select c).FirstOrDefault();
        }
    }
}