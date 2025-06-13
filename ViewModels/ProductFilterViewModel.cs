using Zadatak1.Models;

namespace Zadatak1.ViewModels
{
    public class ProductFilterViewModel
    {
        public string SearchTerm { get; set; }
        public string TypeOfHoney { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public string SortOrder { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
