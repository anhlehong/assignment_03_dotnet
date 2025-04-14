using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepository repository;
        public int pageSize = 4;
        public HomeController(ILogger<HomeController> logger, IStoreRepository repo)
        {
            _logger = logger;
            repository = repo;
        }

        //public IActionResult Index(int productPage = 1)
        //{
        //    return View(new ProductsListViewModel
        //    {
        //        Products = repository.GetProducts
        //        .OrderBy(p => p.ProductID)
        //        .Skip((productPage - 1) * pageSize)
        //        .Take(pageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = productPage,
        //            ItemPerPage = pageSize,
        //            TotalItems = repository.GetProducts.Count()
        //        }

        //    });
        //}

        public IActionResult Index(string category, int productPage = 1)
        {
            var products = repository.GetProducts; 
            return View(new ProductsListViewModel
            {
                Products = products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemPerPage = pageSize,
                    TotalItems = category==null ? products.Count() : products.Where(e=>e.Category==category).Count()
                },
                CurrentCategory = category
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
