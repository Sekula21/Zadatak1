using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productFilterService;
        public HomeController(IProductService productFilterService)
        {
            _productFilterService = productFilterService;
        }
        public async Task<IActionResult> Index(string searchTerm, string typeOfHoney, float? minPrice, float? maxPrice, string sortOrder)
        {
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["TypeSortParam"] = sortOrder == "type" ? "type_desc" : "type";
            ViewData["PriceSortParam"] = sortOrder == "price" ? "price_desc" : "price";

            var filterModel = new ProductFilterViewModel
            {
                SearchTerm = searchTerm,
                Type = typeOfHoney,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortOrder = sortOrder
            };

            var result = await _productFilterService.ApplyFilters(filterModel);
            return View(result);
        }
    }
}
