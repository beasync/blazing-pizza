using BlazingPizza.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Api.Services
{
    public class ProductsService
    {
        private readonly List<Product> products;

        public ProductsService()
        {
            products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Margherita",
                    Price = 12.60m
                },
                new Product
                {
                    Id = 2,
                    Name = "Diavola",
                    Price = 13.60m
                }
            };
        }

        public Task<IEnumerable<Product>> GetProducts()
            => Task.FromResult(products.AsEnumerable());

        public Task<IEnumerable<Product>> SearchProducts(string name)
            => Task.FromResult(products.Where(product => product.Name == name));

        public Task<Product> GetProduct(int id)
            => Task.FromResult(products.SingleOrDefault(product => product.Id == id));

        public Task<int> CreateProduct(Product product)
        {
            var id = products.Count() + 1;

            product.Id = id;

            products.Add(product);

            return Task.FromResult(id);
        }

        public async Task<bool> RemoveProduct(int id)
        {
            var product = await GetProduct(id);

            if (product == default) return false;

            products.Remove(product);

            return true;
        }
    }
}