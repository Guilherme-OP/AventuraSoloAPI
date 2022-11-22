using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public class OrigemDestinoRepository : Repository<OrigemDestino>, IOrigemDestinoRepository
{
    public OrigemDestinoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<OrigemDestino> GetDestinos()
    {
        return Get().Include(od => od.PassoDestino);
    }
}
