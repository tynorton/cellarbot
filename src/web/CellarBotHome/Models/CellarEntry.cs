//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CellarEntry
    {
        public int ID { get; set; }
        public int BeerID { get; set; }
        public int CellarID { get; set; }
        public string Notes { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> Year { get; set; }
    
        public virtual Beer Beer { get; set; }
        public virtual Cellar Cellar { get; set; }
    }
}
