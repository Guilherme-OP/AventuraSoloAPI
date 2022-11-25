using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public class PassoRepository : Repository<Passo>, IPassoRepository
{
    public PassoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Passo>> GetPassosOrigemDestinos()
    {
        return await Get().Include(p => p.Origens).ToListAsync();
    }
}
