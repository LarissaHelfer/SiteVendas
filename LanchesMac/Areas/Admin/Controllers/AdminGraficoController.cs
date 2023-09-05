using LanchesMac.Areas.Admin.builder.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminGraficoController : Controller
{
    private readonly GraficoVendasService _graficoVendas;

    public AdminGraficoController(GraficoVendasService graficoVendas)
    {
        _graficoVendas = graficoVendas ?? throw 
            new ArgumentException(nameof(graficoVendas));
    }

    public JsonResult VendasItens(int dias)
    {
        var itensVendasTotais = _graficoVendas.GetVendasItens(dias);
        return Json(itensVendasTotais);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult VendasMensal()
    {
        return View();
    }

    [HttpGet]
    public IActionResult VendasSemanal()
    {
        return View();
    }
}
