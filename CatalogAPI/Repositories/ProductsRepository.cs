﻿using CatalogAPI.Context;
using CatalogAPI.Models;
using CatalogAPI.Pagination;
using CatalogAPI.Pagination.Filter;
using CatalogAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductsRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IEnumerable<Product> GetProducts(int size)
    {
        return _appDbContext.products.Take(size).AsNoTracking().ToList();
    }

    /*public IEnumerable<Product> GetProductsPagination(ProductsParameters productsParameters)
    {
        return _appDbContext.products
               .OrderBy(p => p.ProductId)
               .Skip((productsParameters.pageNumber - 1) * productsParameters.pageSize)
               .Take(productsParameters.pageSize).ToList();
    }*/

    public PagedList<Product> GetProductsPagination(ProductsParameters productsParameters)
    {
       var products = _appDbContext.products.OrderBy(p => p.ProductId).AsQueryable();
       var orderProducts = PagedList<Product>.ToPagedList(products, productsParameters.pageNumber, productsParameters.pageSize);
       return orderProducts;
    }

    public Product GetProduct(int id)
    {
        var product = _appDbContext.products.FirstOrDefault(p => p.ProductId.Equals(id));
        if (product is null)
              throw new ArgumentNullException(nameof(GetProduct));

        return product;
    }

    public Product CreateProduct(Product product)
    {
       if(product is null)
            throw new ArgumentNullException(nameof(CreateProduct));

       _appDbContext.products.Add(product);
       _appDbContext.SaveChanges();

        return product;
    }

    public Product UpdateProduct(Product product)
    {
        var productResponse = _appDbContext.products.FirstOrDefault(p => p.ProductId.Equals(product.ProductId));

        if (productResponse is null)
            throw new ArgumentNullException(nameof(UpdateProduct));

        productResponse.Name = product.Name;
        productResponse.Price = product.Price;
        productResponse.Description = product.Description;

        _appDbContext.SaveChanges();
        return productResponse;
    }
    public Product DeleteProduct(int id)
    {
        var productResponse = _appDbContext.products.FirstOrDefault(p => p.ProductId.Equals(id));
        if (productResponse is null)
            throw new ArgumentNullException(nameof(DeleteProduct));

        _appDbContext.products.Remove(productResponse);
        _appDbContext.SaveChanges();

        return productResponse;
    }

    public PagedList<Product> GetProductsByFilter(ProductsFilterPrice productsFilterPrice)
    {
        var products = _appDbContext.products.AsNoTracking().AsQueryable();

        if(productsFilterPrice.Price.HasValue && !string.IsNullOrWhiteSpace(productsFilterPrice.Criterion))
        {
            if(productsFilterPrice.Criterion.Equals("maior", StringComparison.OrdinalIgnoreCase)) {
                products = products.Where(p => p.Price > productsFilterPrice.Price.Value);
            }
            if (productsFilterPrice.Criterion.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(p => p.Price < productsFilterPrice.Price.Value);
            }
            if (productsFilterPrice.Criterion.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(p => p.Price == productsFilterPrice.Price.Value);
            }
        }

        var productsFilter = PagedList<Product>.ToPagedList(products, productsFilterPrice.pageNumber, productsFilterPrice.pageSize);
        return productsFilter;
    }
}
