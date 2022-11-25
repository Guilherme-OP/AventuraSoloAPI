using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public interface IAventuraRepository : IRepository<Aventura>
{
    Task<IEnumerable<Aventura>> GetPassosPorAventura();
}
