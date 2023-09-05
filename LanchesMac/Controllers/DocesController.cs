using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class DocesController : Controller
{
    private readonly ITortasRepository _tortasRepository;
    public DocesController(ITortasRepository tortasRepository)
    {
        _tortasRepository = tortasRepository;
    }

    public IActionResult List(string categoria)
    {
        IEnumerable<Torta> tortas;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(categoria))
        {
            tortas = _tortasRepository.Tortas.OrderBy(t => t.DoceId);
            categoriaAtual = "Todos os doces";
        }
        else
        {
            tortas = _tortasRepository.Tortas
                          .Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                          .OrderBy(c => c.Nome);
        }
        var docesListViewModel = new DoceListViewModel
        {
            Tortas = tortas,
            CategoriaAtual = categoriaAtual
        };

        return View(docesListViewModel);
	}
    public IActionResult Details(int doceId)
    {
        var doce = _tortasRepository.Tortas.FirstOrDefault(t => t.DoceId == doceId);
            return View(doce);
    }

    public ViewResult Search(string searchString)
    {
        IEnumerable<Torta> tortas;
        string categoriaAtual = string.Empty;

        if(string.IsNullOrEmpty(searchString))
        {
            tortas = _tortasRepository.Tortas.OrderBy(t => t.DoceId);
            categoriaAtual = "Todos os Doces";
        }
        else
        {
            tortas = _tortasRepository.Tortas
                .Where(t => t.Nome.ToLower().Contains(searchString.ToLower()));
            if (tortas.Any()) {
				categoriaAtual = "Produtos";
			}
            else
            {
                categoriaAtual = "Nenhum doce foi encontrado";
            }
        }
        return View("~/Views/Doces/List.cshtml", new DoceListViewModel
        {
            Tortas = tortas,
            CategoriaAtual = categoriaAtual
        });
    }
}
