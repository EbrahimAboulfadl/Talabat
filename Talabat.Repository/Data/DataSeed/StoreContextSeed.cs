using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.DataSeed
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext) {

            var brandsJson = File.ReadAllText("..\\Talabat.Repository\\Data\\DataSeed\\Brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);
            if (brands?.Count>0) {
                foreach (var brand in brands) { 

                dbContext.Set<ProductBrand>().Add(brand);
                
                }
                await dbContext.SaveChangesAsync();
            }  
            
            var typesJson = File.ReadAllText("..\\Talabat.Repository\\Data\\DataSeed\\Types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesJson);
            if (types?.Count>0) {
                foreach (var type in types) { 

                dbContext.Set<ProductType>().Add(type);
                
                }
                await dbContext.SaveChangesAsync();
            }      
            
            var productsJson = File.ReadAllText("..\\Talabat.Repository\\Data\\DataSeed\\Products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson);
            if (products?.Count>0) {
                foreach (var product in products) { 

                dbContext.Set<Product>().Add(product);
                
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
