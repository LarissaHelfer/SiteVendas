using LanchesMac.Models;

namespace LanchesMac.ViewModels;

public class DoceListViewModel
{
	public IEnumerable<Torta> Tortas { get; set; }	
	public string CategoriaAtual { get; set; }
}
