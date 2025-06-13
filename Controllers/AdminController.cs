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
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _userRepository;

        public AdminController(IRepository<Product> productRepository, IRepository<User> userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;   
        }
        public async Task<IActionResult> Dashboard()
        {
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }

        

        public async Task<IActionResult> Products()
        {
            return View(await _productRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync(id.Value);
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
                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();
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

            var product = await _productRepository.GetByIdAsync(id.Value);
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
                    _productRepository.Update(product);
                    await _productRepository.SaveChangesAsync();
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

            var product = await _productRepository.GetAllAsync();
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
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                _productRepository.Delete(product);
                await _productRepository.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Products));
        }

        private async Task<bool> ProductExists(Guid id)
        {
            return await _productRepository.AnyAsync(p => p.Id == id);
        }
    }
}
