using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LanchesMac.ViewModels;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;

namespace LanchesMac.Controllers;

public class CarrinhoCompraController : Controller
{
	private readonly ITortasRepository _tortasRepository;
	private readonly CarrinhoCompra _carrinhoCompra;

	public CarrinhoCompraController(ITortasRepository tortasRepository, CarrinhoCompra carrinhoCompra)
	{
		_tortasRepository = tortasRepository;
		_carrinhoCompra = carrinhoCompra;
	}

	public IActionResult Index()
	{
		var itens = _carrinhoCompra.GetCarrinhoCompraItens();
		_carrinhoCompra.CarrinhoCompraItems = itens;

		var carrinhoCompraVM = new CarrinhoCompraViewModel
		{
			CarrinhoCompra = _carrinhoCompra,
			CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
		};
	return View(carrinhoCompraVM);
	}

    [Authorize]
    public IActionResult AdicionarItemNoCarrinhoCompra(int doceId)
	{
		var doceSelecionado = _tortasRepository.Tortas.FirstOrDefault(p=> p.DoceId ==  doceId);

		if(doceSelecionado != null)
		{
			_carrinhoCompra.AdicionarAoCarrinho(doceSelecionado);
		}
		return RedirectToAction("Index");
	}

    [Authorize]
    public IActionResult RemoverItemDoCarrinhoCompra(int doceId)
	{
		var lancheSelecionado = _tortasRepository.Tortas
								.FirstOrDefault(p => p.DoceId == doceId);

		if (lancheSelecionado != null)
		{
			_carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
		}
		return RedirectToAction("Index");
	}
}
