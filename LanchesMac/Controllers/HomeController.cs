using System.Diagnostics;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITortasRepository _tortasRepository;
        private List<Categoria> GetCategories()
        {
            var categories = new List<Categoria>
        {
            new Categoria { CategoriaId = 1, CategoriaNome = "Fatia de torta" },
            new Categoria { CategoriaId = 2, CategoriaNome = "Torta Inteira" },
            new Categoria { CategoriaId = 2, CategoriaNome = "Cucas" },
            new Categoria { CategoriaId = 2, CategoriaNome = "Bebidas" },
            // Add more categories as needed
        };
            return categories;
        }

        public HomeController(ITortasRepository tortasRepository)
		{
			_tortasRepository = tortasRepository;
		}

		public IActionResult Index()
        {
            var viewModel = new HomeViewModel();
            viewModel.Categorias = GetCategories(); // Populate Categories with your category data
            return View(viewModel);
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