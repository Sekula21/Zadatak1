using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.Services;
using Zadatak1.ViewModels;

namespace Zadatak1.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ExceptionHandler exceptionHandler):base(exceptionHandler)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Products()
        {
            var product = await TryExecute(() => _productService.GetAll());
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await TryExecute(() => _productService.GetById(id.Value));
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            if (ModelState.IsValid)
            {
                await TryExecute(() => _productService.Create(model));
                return RedirectToAction(nameof(Products));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null) return NotFound();

            var (success, product) = await TryExecuteWithResult(() => _productService.GetById(id));
            if (!success || product == null) return NotFound();

            var model = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Producer = product.Producer,
                Adress = product.Adress,
                Color = product.Color,
                Description = product.Description,
                Price = product.Price,
                Amount = product.Amount
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, Product model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var updated = await TryExecute(() => _productService.Update(id, model));
                if (updated == null) return NotFound();

                return RedirectToAction(nameof(Products));
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var product = await TryExecute(() => _productService.GetById(id.Value));
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deleted = await TryExecute(() => _productService.Delete(id));
            if (deleted == null) return NotFound();

            return RedirectToAction(nameof(Products));
        }
        private async Task<bool> ProductExists(Guid id)
        {
            var (success, exist) = await TryExecuteWithResult(() => _productService.Any(p => p.Id == id));
            return exist;
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

            var result = await TryExecute(() => _productService.ApplyFilters(filterModel));
            return View(result);
        }
    }
}
