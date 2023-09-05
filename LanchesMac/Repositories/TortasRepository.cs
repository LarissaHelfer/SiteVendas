using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories
{
    public class TortasRepository : ITortasRepository
    {
        private readonly AppDbContext _context;
        public TortasRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Torta> Tortas => _context.Tortas.Include(c => c.Categoria);

        public IEnumerable<Torta> TortasPreferidas => _context.Tortas.Where(p=>p.IsLanchePreferido).Include(c=>c.Categoria);

        public Torta GetTortaById(int tortaId)
        {
            return _context.Tortas.FirstOrDefault(d => d.DoceId == tortaId);
        }
    }
}
