using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public class AventuraRepository : Repository<Aventura>, IAventuraRepository
{
    public AventuraRepository(AppDbContext context) : base(context) { }

    public IEnumerable<Aventura> GetPassosPorAventura()
    {
        return Get().Include(a => a.Passos);
    }
}
