using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public interface IAventuraRepository : IRepository<Aventura>
{
    IEnumerable<Aventura> GetPassosPorAventura();
}
