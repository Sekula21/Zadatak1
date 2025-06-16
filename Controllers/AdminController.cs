using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var users = await _adminService.GetAllUsersAsync();
            return View(users);
        }



        public async Task<IActionResult> Products()
        {
            return View(await _adminService.GetAllProductsAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _adminService.GetByIdProductsAsync(id.Value);
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
        public async Task<IActionResult> Create([Bind("Id,Name,TypeOfHoney,Producer,Adress,Color,Description,PricePerJar,TotalAmount")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _adminService.AddProductsAsync(product);
                await _adminService.SaveChangesProductsAsync();
                return RedirectToAction(nameof(Products));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _adminService.GetByIdProductsAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,TypeOfHoney,Producer,Adress,Color,Description,PricePerJar,TotalAmount")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _adminService.UpdateProducts(product);
                    await _adminService.SaveChangesProductsAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Products));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _adminService.GetAllProductsAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _adminService.GetByIdProductsAsync(id);
            if (product != null)
            {
                _adminService.DeleteProducts(product);
                await _adminService.SaveChangesProductsAsync();
            }
            return RedirectToAction(nameof(Products));
        }

        private async Task<bool> ProductExists(Guid id)
        {
            return await _adminService.AnyProductsAsync(p => p.Id == id);
        }
    }
}
