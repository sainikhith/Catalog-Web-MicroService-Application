using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            #region If there is no data in the database data is seeded on program startup
            if (!existProduct)
            {
                productCollection.InsertManyAsync(getPreConfiguredProduct());
            }
            #endregion
        }

        private static IEnumerable<Product> getPreConfiguredProduct()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "Iphone SE",
                    Summary = "asd",
                    Description = "bsd",
                    Category = "Cata",
                    ImageFile = "p1.jpg",
                    Price = 54.93M
                },
                new Product()
                {
                    Name = "Iphone X",
                    Summary = "asd",
                    Description = "bsd",
                    Category = "Cata",
                    ImageFile = "p1.jpg",
                    Price = 984.93M
                },
                new Product()
                {
                    Name = "Iphone 12",
                    Summary = "asd",
                    Description = "bsd",
                    Category = "Cata",
                    ImageFile = "p1.jpg",
                    Price = 1006.93M
                },
                new Product()
                {
                    Name = "Ipone 6s",
                    Summary = "asd",
                    Description = "bsd",
                    Category = "Cata",
                    ImageFile = "p1.jpg",
                    Price = 54.93M
                }
        };
        }
    }
}
