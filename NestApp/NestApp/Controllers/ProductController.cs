﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestApp.DAL;

namespace NestApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.
                Include(x => x.Category).
                Include(x => x.ProductImages).
                Take(5).ToListAsync());
        }
        public async Task<IActionResult> LoadProduct(int skip)
        {
            return PartialView("_ProductPartial", await _context.Products.
                Include(x => x.Category).
                Include(x => x.ProductImages).Skip(skip).Take(5).ToListAsync());
        }

    }
}
