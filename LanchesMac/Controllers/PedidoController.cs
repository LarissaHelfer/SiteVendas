using System.Drawing.Text;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class PedidoController : Controller
{
	private readonly IPedidoRepository _pedidoRepository;
	private readonly CarrinhoCompra _carrinhoCompra;

	public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
	{
		_pedidoRepository = pedidoRepository;
		_carrinhoCompra = carrinhoCompra;
	}

    [Authorize]
    [HttpGet]
	public IActionResult Checkout()
	{
		return View();
	}

    [Authorize]
    [HttpPost]
	public IActionResult Checkout(Pedido pedido)
	{
		int totalItensPedido = 0;
		decimal precoTotalPedido = 0.0m;

		//obtem os itens do carrinho do cliente
		List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
		_carrinhoCompra.CarrinhoCompraItems = items;

		//verifica se existem itens do pedido
		if (_carrinhoCompra.CarrinhoCompraItems.Count == 0)
		{
			ModelState.AddModelError("", "Seu carrinho está vazio.");
		}

		//calcula o total de itens e o total do pedido
		foreach(var item in items)
		{
			totalItensPedido += item.Quantidade;
			precoTotalPedido += (item.Torta.Preco * item.Quantidade);
		}

		//atribui os valores obtidos
		pedido.TotalItensPedido = totalItensPedido;
		pedido.PedidoTotal = precoTotalPedido;

		//valida os dados do pedido
		if (ModelState.IsValid)
		{
			//cria o pedido e os detalhes dele
			_pedidoRepository.CriarPedido(pedido);

			//define as mensagem ao cliente
			ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido";
			ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

			//limpa o carrinho
			_carrinhoCompra.LimparCarrrinho();

			//retorna na view com dados do cliente e do pedido
			return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
		}
		return View(pedido);
	}
}
