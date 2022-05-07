
using BLL.FactoryRepo;
using BLL.ProductRepo;
using BLL.Repo;
using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace EcommUnitTesting
{
    public class ProductTest
    {
        DataBaseContext context;
        IProductRepo productRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(databaseName: "ECommApp").Options;
            context = new DataBaseContext(options);
            // Insert seed data into the database using one instance of the context
            context.Users.Add(new User { Id = 1, Name = "Atiksha", ContactNumber = 9466287901, Email = "a@gmail.com", Role = "Customer" });
            context.Users.Add(new User { Id = 2, Name = "Akanksha", ContactNumber = 9466287111, Email = "aka@gmail.com", Role = "Admin" });
            productRepo = new ProductRepo(context);
            context.Productss.Add(new Products { pId = 1, cId = 1, pName = "P1", Price = 1200, Description = "D1", Quantity = 5, Status = "Active", DateofCreation = System.DateTime.Now });
            context.Productss.Add(new Products { pId = 2, cId = 2, pName = "P2", Price = 1500, Description = "D2", Quantity = 15, Status = "Active", DateofCreation = System.DateTime.Now });
            context.SaveChanges();
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }
        [Test]
        [Order(1)]
        public void GetProductTest()
        {
            var product = productRepo.GetProducts();
            Assert.AreEqual(product.Count, 2);
        }
        [Test]
        [Order(2)]
        public void UpdateProductTest()
        {
            var productData = context.Productss.Find(1);
            var product = productRepo.GetProducts();
            productData.pName = "MI";
            productRepo.UpdateProduct(productData,2);
            Assert.AreEqual(productData.pName, "MI");
        }
        [Test]
        [Order(4)]
        public void DeleteProductTest()
        {
            productRepo.DeleteProduct(1,2);
            var product = productRepo.GetProducts();
            Assert.AreEqual(product.Count, 3);
        }

        [Test]
        [Order(3)]
        public void AddProductTest()
        {
            var products = new Products();
            products.pId = 3;
            products.pName = "p3";
            products.Quantity = 12;
            products.DateofCreation= System.DateTime.Now;
            products.Description = "xyz";
            products.Status="Inactive";
            products.cId = 3;
            products.Price = 1999;
            List<Products>product1=new List<Products>();   
            product1.Add(products);
            productRepo.AddProduct(product1,2);
            var product2 = productRepo.GetProducts();
            Assert.AreEqual(product2.Count, 3);



        }
    }
}