using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models;

public class CarrinhoCompra
{
    private readonly AppDbContext _context;

	public CarrinhoCompra(AppDbContext context)
	{
		_context = context;
	}

	public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

	public static CarrinhoCompra GetCarrinho(IServiceProvider services)
	{
		//define uma sessão
		ISession session=services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

		//obtem um serviço do tipo do nosso contexto
		var context = services.GetService<AppDbContext>();

		//obtem ou gera um ID para o carrinho
		string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

		//atrinui o ID do carrinho na sessão
		session.SetString("CarrinhoId", carrinhoId);

		//retorna o carrinho com o contexto e o Id atrinuido ou obtido
		return new CarrinhoCompra(context)
		{
			CarrinhoCompraId = carrinhoId
		};
	}
	public void AdicionarAoCarrinho(Torta torta)
	{
		var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
			s => s.Torta.DoceId == torta.DoceId &&
			s.CarrinhoCompraId == CarrinhoCompraId);
		if(carrinhoCompraItem==null)
		{
			carrinhoCompraItem = new CarrinhoCompraItem()
			{
				CarrinhoCompraId = CarrinhoCompraId,
				Torta = torta,
				Quantidade = 1
			};
			_context.CarrinhoCompraItens.Add(carrinhoCompraItem);
		}
		else
		{
			carrinhoCompraItem.Quantidade++;
		}
		_context.SaveChanges();
	}

	public int RemoverDoCarrinho(Torta torta)
	{
		var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
			s => s.Torta.DoceId == torta.DoceId &&
			s.CarrinhoCompraId == CarrinhoCompraId);
		var quantidadeLocal = 0;

		if(carrinhoCompraItem!=null)
		{
			if (carrinhoCompraItem.Quantidade < 1)
			{
				carrinhoCompraItem.Quantidade--;
				quantidadeLocal = carrinhoCompraItem.Quantidade;
			}
			else
			{
				_context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
			}
		}
		_context.SaveChanges();
		return quantidadeLocal;
	}

	public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
	{
		return CarrinhoCompraItems ??
			(CarrinhoCompraItems =
				_context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
				.Include(s => s.Torta)
				.ToList());
	}

	public void LimparCarrrinho()
	{
		var carrinhoItens = _context.CarrinhoCompraItens.Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
		_context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
		_context.SaveChanges();
	}

	public decimal GetCarrinhoCompraTotal()
	{
		var total = _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId).Select(c => c.Torta.Preco * c.Quantidade).Sum();
		return total;
	}

	
}
