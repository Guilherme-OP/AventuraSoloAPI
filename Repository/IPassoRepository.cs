using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public interface IPassoRepository : IRepository<Passo>
{
    Task<IEnumerable<Passo>> GetPassosOrigemDestinos();
}
