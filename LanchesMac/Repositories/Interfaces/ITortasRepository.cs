using LanchesMac.Models;

namespace LanchesMac.Repositories.Interfaces;

public interface ITortasRepository
{
    IEnumerable<Torta> Tortas { get; }
    IEnumerable<Torta> TortasPreferidas { get; }
    Torta GetTortaById(int tortaId);
}
