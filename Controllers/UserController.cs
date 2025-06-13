using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        public UserController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Products()
        {
            return View(await _productRepository.GetAllAsync());
        }
        

    }
}
