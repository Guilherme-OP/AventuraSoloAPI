namespace SoloAdventureAPI.Repository;

public interface IUnitOfWork
{
    IAventuraRepository AventuraRepository { get; }
    IPassoRepository PassoRepository { get; }
    IOrigemDestinoRepository OrigemDestinoRepository { get; }

    Task Commit();
}
