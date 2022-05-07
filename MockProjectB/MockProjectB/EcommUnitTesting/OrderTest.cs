using BLL.Repo;
using BusinessLayer.Repositories;
using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace EcommUnitTesting
{
    public class OrderTest
    {
        DataBaseContext context;
        IOrderRepo orderRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "MyApp")
            .Options;
            context = new DataBaseContext(options);
            // Insert seed data into the database using one instance of the context
            orderRepo = new OrderRepo(context);
            context.OrderDetailss.Add(new OrderDetails { DetailsId=1, OId=1, ProductId=1, Quantity=1 });
            context.OrderDetailss.Add(new OrderDetails { DetailsId = 2, OId = 2, ProductId = 2, Quantity = 2 });
            context.Users.Add(new User { Id = 1, Name = "Atiksha", ContactNumber = 9466287901, Email = "a@gmail.com", Role = "Customer",Address = "ADD1", City = "c1", Pincode = 282002, State = "S1" });
            context.Users.Add(new User { Id = 2, Name = "Akanksha", ContactNumber = 9466287111, Email = "aka@gmail.com", Role = "Admin", Address = "ADD2", City = "c2", Pincode = 281002, State = "S2" });
            context.Productss.Add(new Products { pId = 1, cId = 1, pName = "P1", Price = 1200, Description = "D1", Quantity = 5, Status = "Active", DateofCreation = System.DateTime.Now });
            context.Productss.Add(new Products { pId = 2, cId = 2, pName = "P2", Price = 1500, Description = "D2", Quantity = 15, Status = "Active", DateofCreation = System.DateTime.Now });
            context.CartProducts.Add(new CartProduct { CartId = 1, pId = 1, Quantity = 20, Sno = 1 });
            context.CartProducts.Add(new CartProduct { CartId = 2, pId = 2, Quantity = 10, Sno = 2 });
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }
        [Test]
        [Order(1)]
        public void GetOrderTest()
        {
            var order = orderRepo.GetOrderList(1);
            Assert.AreEqual(order.UserName,"Atiksha");
        }
    }
}