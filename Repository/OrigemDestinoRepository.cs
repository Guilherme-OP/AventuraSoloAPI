using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public class OrigemDestinoRepository : Repository<OrigemDestino>, IOrigemDestinoRepository
{
    public OrigemDestinoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrigemDestino>> GetDestinos()
    {
        return await Get().Include(od => od.PassoDestino).ToListAsync();
    }
}
