using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalWineParser.Model.TotalWine
{
    public class Image
    {
        public string Description { get; set; }
        public string SmallUrl { get; set; }
        public object LargeUrl { get; set; }
    }

    public class Pricing
    {
        public object AlternateLabel { get; set; }
        public string Label { get; set; }
        public double Value { get; set; }
        public string Currency { get; set; }
        public string Formatted { get; set; }
        public int SortOrder { get; set; }
        public bool IsStrikeThrough { get; set; }
    }

    public class ProductRating
    {
        public int Points { get; set; }
        public string Target { get; set; }
    }

    public class Product
    {
        public string AdditionalCharges { get; set; }
        public object AvailabilityMessage { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public object InStoreLocation { get; set; }
        public string Description { get; set; }
        public List<string> Notes { get; set; }
        public List<Pricing> Pricing { get; set; }
        public ProductRating ProductRating { get; set; }
        public string Size { get; set; }
        public string SkuId { get; set; }
        public object SkuSelection { get; set; }
        public string SubHeader { get; set; }
        public bool IsInStock { get; set; }
        public int VintageYear { get; set; }
    }

    public class Pager
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int RecordCount { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }

    public class SortOrderOption
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsSelected { get; set; }
    }

    public class Sorter
    {
        public List<SortOrderOption> SortOrderOptions { get; set; }
        public object SortBy { get; set; }
        public bool IsAscending { get; set; }
    }

    public class FilterOption
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public object Note { get; set; }
        public int Results { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsSelected { get; set; }
    }

    public class FilterSection
    {
        public string Label { get; set; }
        public object Note { get; set; }
        public List<FilterOption> FilterOptions { get; set; }
        public bool IsCollapsed { get; set; }
    }

    public class Filter
    {
        public string BreadCrumbHTML { get; set; }
        public List<FilterSection> FilterSections { get; set; }
        public bool hasBreadCrumb { get; set; }
        public List<string> BreadCrumbs { get; set; }
    }

    public class Resultset
    {
        public List<object> Errors { get; set; }
        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }
        public Sorter Sorter { get; set; }
        public Filter Filter { get; set; }
    }

    public class RootObject
    {
        public bool success { get; set; }
        public Resultset resultset { get; set; }
        public List<object> errors { get; set; }
        public List<object> warnings { get; set; }
    }
}
