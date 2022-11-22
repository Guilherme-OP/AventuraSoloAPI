using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public interface IPassoRepository : IRepository<Passo>
{
    IEnumerable<Passo> GetPassosOrigemDestinos();
}
