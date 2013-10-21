﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CellarBotHome.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CellarBotEntities : DbContext
    {
        public CellarBotEntities()
            : base("name=CellarBotEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Beer> Beers { get; set; }
        public virtual DbSet<Brewery> Breweries { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Style> Styles { get; set; }
    
        public virtual ObjectResult<sp_searchStyles_Result> sp_searchStyles(string searchValue)
        {
            var searchValueParameter = searchValue != null ?
                new ObjectParameter("SearchValue", searchValue) :
                new ObjectParameter("SearchValue", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_searchStyles_Result>("sp_searchStyles", searchValueParameter);
        }
    }
}
