using System.Collections.Generic;
using EShopService.Data.Models;

namespace EShopService.Data.Mocks
{
    public static class MockEShopServiceContext
    {
        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Mobile Phone",
                ImgUri = "ourhost.static.com/img/products/eac756f.png",
                Price = 12548.5m,
                Description = "The best mobile phone, that you have ever seen"
            },
            new Product
            {
                Id = 2,
                Name = "Plazma TV",
                ImgUri = "ourhost.static.com/img/products/6785ff1.png",
                Price = 45612.1m,
                Description = "Good enough plazma TV"
            },
            new Product
            {
                Id = 3,
                Name = "Tablet PC",
                ImgUri = "ourhost.static.com/img/products/facb55d.png",
                Price = 19003m,
                Description = "Tablet PC for navigating the net and watching movies"
            },
            new Product
            {
                Id = 4,
                Name = "Laptop",
                ImgUri = "ourhost.static.com/img/products/9870e76.png",
                Price = 30221.9m,
                Description = "Average laptop for every day working"
            },
            new Product
            {
                Id = 5,
                Name = "Stationare PC",
                ImgUri = "ourhost.static.com/img/products/abf9eed.png",
                Price = 40991.8m,
                Description = "Good choise for gaming"
            },new Product
            {
                Id = 6,
                Name = "Fridge",
                ImgUri = "ourhost.static.com/img/products/999aaef.png",
                Price = 50124.4m,
                Description = "Very big fridge for big family"
            },
            new Product
            {
                Id = 7,
                Name = "Iron",
                ImgUri = "ourhost.static.com/img/products/654fa55.png",
                Price = 2034.9m,
                Description = "Regular iron for regular clothes"
            },
            new Product
            {
                Id = 8,
                Name = "Microwave oven",
                ImgUri = "ourhost.static.com/img/products/12345ab.png",
                Price = 3000m,
                Description = "900W microwave oven"
            },
            new Product
            {
                Id = 9,
                Name = "WiFi Router",
                ImgUri = "ourhost.static.com/img/products/eaa456f.png",
                Price = 2555.5m,
                Description = "Good WPA wireless WiFi router with strong signal"
            },
            new Product
            {
                Id = 10,
                Name = "Happyness",
                ImgUri = "ourhost.static.com/img/products/7777777.png",
                Price = 777.77m,
                Description = "Real happyness that you can afford"
            }
        };
    }
}