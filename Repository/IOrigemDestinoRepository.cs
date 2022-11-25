using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public interface IOrigemDestinoRepository : IRepository<OrigemDestino>
{
    Task<IEnumerable<OrigemDestino>> GetDestinos();
}
