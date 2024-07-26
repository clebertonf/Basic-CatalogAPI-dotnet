using CatalogAPI.Context;
using CatalogAPI.Repositories.Interfaces;
namespace CatalogAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ICustomerRepository? _customerRepository;

    public AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public ICustomerRepository CustomerRepository
    {
        get
        {
            return _customerRepository ?? new CustomerRepository(_appDbContext);
        }
    }

    public void Commit()
    {
        _appDbContext.SaveChanges();
    }

    public void Dispose() 
    {
        _appDbContext.Dispose();
    }
}
