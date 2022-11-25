using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public class AventuraRepository : Repository<Aventura>, IAventuraRepository
{
    public AventuraRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Aventura>> GetPassosPorAventura()
    {
        return await Get().Include(a => a.Passos).ToListAsync();
    }
}
