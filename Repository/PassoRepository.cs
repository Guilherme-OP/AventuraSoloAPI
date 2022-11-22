using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public class PassoRepository : Repository<Passo>, IPassoRepository
{
    public PassoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Passo> GetPassosOrigemDestinos()
    {
        return Get().Include(p => p.Origens);
    }
}
