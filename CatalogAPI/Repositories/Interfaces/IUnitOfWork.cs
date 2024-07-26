namespace CatalogAPI.Repositories.Interfaces;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
    void Commit();
}
