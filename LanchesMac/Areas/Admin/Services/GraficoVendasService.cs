using System.ComponentModel.DataAnnotations;
using LanchesMac.Context;
using LanchesMac.Models;

namespace LanchesMac.Areas.Admin.builder.Services;

public class GraficoVendasService
{
    private readonly AppDbContext context;

    public GraficoVendasService(AppDbContext context)
    {
        this.context = context;
    }

    public List<ItensGrafico> GetVendasItens(int dias = 360)
    {
        var data = DateTime.Now.AddDays(-dias);

        var itens = (from pd in context.PedidoDetalhes
                     join i in context.Tortas on pd.LancheId equals i.DoceId
                     where pd.Pedido.PedidoEnviado >= data
                     group pd by new { pd.LancheId, i.Nome}
                     into g
                     select new
                     {
                         ItemNome = g.Key.Nome,
                         ItemQuantidade = g.Sum(q => q.Quantidade),
                         ItemValorTotal = g.Sum(a => a.Preco * a.Quantidade)
                     });
        var lista = new List<ItensGrafico>();

        foreach (var item in itens)
        {
            var ytem = new ItensGrafico();
            ytem.ItemNome = item.ItemNome;
            ytem.ItemQuantidade = item.ItemQuantidade;
            ytem.ItemValorTotal = item.ItemValorTotal;
            lista.Add(ytem);
        }
        return lista;
    }
}
