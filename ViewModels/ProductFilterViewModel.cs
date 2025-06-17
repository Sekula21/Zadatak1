using Zadatak1.Models;

namespace Zadatak1.ViewModels
{
    public class ProductFilterViewModel
    {
        public string SearchTerm { get; set; }
        public string Type { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public string SortOrder { get; set; }

        public IEnumerable<ProductEditViewModel> Products { get; set; }
    }
}
