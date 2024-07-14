namespace CatalogAPI.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IProductsRepository ProductsRepository { get; }
        ICategoriesRepository CategoriesRepository { get; }
        void Commit();
    }
}
