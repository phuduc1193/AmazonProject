using Common.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace EmailService.UnitTest
{
    public class EmailSendingTest
    {
        [Fact]
        public void Test1()
        {

            var Order = new Order()
            {
                Id = 234,
                OrderDate = new DateTime(2018, 07, 11),
                Items = new List<ProductItem>() {
                        new ProductItem()
                        {
                            Product = new Product
                            {
                                Id = 3982,
                                Name = "Samsung TV 4k",
                                Category = new Category
                                {
                                    Id = 1, Name ="Electronic"
                                },
                                Description = "Another Samsung TV 4k that will blow up your eyes"
                            },
                            Quantity = 1,
                            UnitPrice = 998,
                        },
                        new ProductItem()
                        {
                            Product = new Product
                            {
                                Id = 4792,
                                Name = "Harry Potter and the Socerer Stone",
                                Category = new Category
                                {
                                    Id = 2, Name ="Book"
                                },
                                Description = "Harry Potter and the Socerer Stone"
                            },
                            Quantity = 1,
                            UnitPrice = 10,
                        }
                    },
                TotalPrice = 998,
                User = new User()
                {
                    Id = 45065,
                    Name = "Kha Tran",
                    Email = "soltran14@hotmail.com",
                    Address = "13227 Babbitt St",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77034",
                    Country = "USA"
                    
                }
            };

            var orderJson = Newtonsoft.Json.JsonConvert.SerializeObject(Order);

        }
    }
}
