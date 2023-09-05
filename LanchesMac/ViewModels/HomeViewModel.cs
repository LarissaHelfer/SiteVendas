using LanchesMac.Models;

namespace LanchesMac.ViewModels;

public class HomeViewModel
{
	public IEnumerable<Torta> TortasPreferidas { get; set; }

    public List<Categoria> Categorias { get; set; }
}
