
using Api.Models;

namespace Api.Classes
{
    public class ProductQueryParameters : QueryParameters<Product>
    {
        public string Sku { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Search { get; set; }
        internal bool HasMinAndMaxPrice
        {
            get { return MinPrice != null && MaxPrice != null; }
        }
        internal bool HasSku { get{ return string.IsNullOrEmpty(Sku) == false;} }
        internal bool HasSearch { get{ return string.IsNullOrEmpty(Search) == false;} }

    }
}
