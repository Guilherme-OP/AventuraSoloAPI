using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public interface IOrigemDestinoRepository : IRepository<OrigemDestino>
{
    IEnumerable<OrigemDestino> GetDestinos();
}
