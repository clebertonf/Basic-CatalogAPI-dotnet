using CatalogAPI.Context;
using CatalogAPI.Models;
using CatalogAPI.Repositories.Generic;

namespace CatalogAPI.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
