using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CellarBotHome.Models
{
    public static class CellarHelper
    {
        public static IEnumerable<Cellar> GetUserCellars(string userId)
        {
            var ents = new CellarBotHome.Models.CellarBotEntities();
            return (from c in ents.Cellars
                           where c.UserID == userId
                           select c)
                .OrderBy(obj => obj.Name)
                .ToList();
        }

        public static Cellar GetCellar(int cellarId)
        {
            var ents = new CellarBotHome.Models.CellarBotEntities();
            return (from c in ents.Cellars
                    where c.ID == cellarId
                    select c).FirstOrDefault();
        }
    }
}