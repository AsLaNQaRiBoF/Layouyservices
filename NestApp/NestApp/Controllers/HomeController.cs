using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestApp.DAL;
using NestApp.Models;
using NestApp.ViewModel;
using HomeVN = NestApp.ViewModel.HomeVN;

namespace NestApp.Controllers
{
    public class HomeController : Controller
	{
		private readonly AppDbContext _context;

		public HomeController(AppDbContext context)
		{
			_context=context;
		}

        public List<Category> RandomCategories { get; private set; }
        public List<Product> TopRatedProducts { get; private set; }
        public List<Product> RecentProducts { get; private set; }
        public List<Slider> Sliders { get; private set; }
        public List<Category> PopularCategories { get; private set; }

        public async Task<IActionResult> Index()
		{
			var sliders= await _context.Sliders.ToListAsync();
			return View(sliders);

		}
        public async Task<IActionResult> Index()
        {

            HomeVM homeVM = new HomeVM();
            {
                Sliders = await _context.Sliders.ToListAsync(),
                PopularCategories = await _context.Categories.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Products.Count).ToListAsync(),
                RandomCategories = await _context.Categories.Where(x => x.IsDeleted == false).OrderBy(x => Guid.NewGuid()).ToListAsync(),
                TopRatedProducts = await _context.Products.Include(x => x.ProductImages).Where(x => x.IsDeleted == false).OrderByDescending(p => p.Rating).Take(3).ToListAsync(),
                RecentProducts = await _context.Products.Include(x => x.ProductImages).Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Take(3).ToListAsync();
               }
            ;
            return View(homeVM);
        }
    }      
 }

    

