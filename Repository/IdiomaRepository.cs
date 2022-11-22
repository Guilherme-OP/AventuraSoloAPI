using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public class IdiomaRepository : Repository<Idioma>, IIdiomaRepository
{
    public IdiomaRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Idioma> GetAventurasPorIdioma()
    {
        return Get().Include(i => i.Aventuras);
    }
}
