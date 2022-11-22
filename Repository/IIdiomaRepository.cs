using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public interface IIdiomaRepository : IRepository<Idioma>
{
    IEnumerable<Idioma> GetAventurasPorIdioma();
}
