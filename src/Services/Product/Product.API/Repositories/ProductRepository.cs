using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories;

public class ProductRepository : RepositoryBaseAsync<Entities.Product, long, ProductContext>, IProductRepository
{
    // ReSharper disable once ConvertToPrimaryConstructor
    public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<IEnumerable<Entities.Product>> GetProducts() => await FindAll().ToListAsync();

    public Task<Entities.Product?> GetProduct(long id) => GetByIdAsync(id);

    public Task<Entities.Product?> GetProductByNo(string productNo) =>
        FindByCondition(x => x.No.Equals(productNo)).SingleOrDefaultAsync();

    public Task CreateProduct(Entities.Product product) => CreateAsync(product);

    public Task UpdateProduct(Entities.Product product) => UpdateAsync(product);

    public async Task DeleteProduct(long id)
    {
        var product = await GetProduct(id);
        if (product != null) await DeleteAsync(product);
    }
    
}