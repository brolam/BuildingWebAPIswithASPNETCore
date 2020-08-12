
namespace Api.Classes
{
    public class ProductQueryParameters : QueryParameters
    {
        public string Sku { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Search { get; set; }
        public bool HasMinAndMaxPrice
        {
            get { return MinPrice != null && MaxPrice != null; }
        }
        public bool HasSku { get{ return string.IsNullOrEmpty(Sku) == false;} }
        public bool HasSearch { get{ return string.IsNullOrEmpty(Search) == false;} }

    }
}
