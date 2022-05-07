
using BLL.ProductRepo;
using BLL.Repo;
using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;namespace EcommUnitTesting
{
    public class CartTest
    {
        DataBaseContext context;
        ICartRepository cartRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(databaseName: "ECommApp").Options;
            context = new DataBaseContext(options);
            // Insert seed data into the database using one instance of the context
            context.Users.Add(new User { Id = 1, Name = "Atiksha", ContactNumber = 9466287901, Email = "a@gmail.com", Role = "Customer", Address="ADD1", City="c1", Pincode=282002, State="S1" });
            context.Users.Add(new User { Id = 2, Name = "Akanksha", ContactNumber = 9466287111, Email = "aka@gmail.com", Role = "Admin", Address = "ADD2", City = "c2", Pincode = 222002, State = "S2" });
            context.Productss.Add(new Products { pId = 1, cId = 1, pName = "P1", Price = 1200, Description = "D1", Quantity = 5, Status = "Active", DateofCreation = System.DateTime.Now });
            context.Productss.Add(new Products { pId = 2, cId = 2, pName = "P2", Price = 1500, Description = "D2", Quantity = 15, Status = "Active", DateofCreation = System.DateTime.Now });
            cartRepo = new CartRepo(context);
            context.CartProducts.Add(new CartProduct { CartId = 1, pId = 1, Quantity = 20, Sno=1 }) ;
            context.CartProducts.Add(new CartProduct { CartId = 2, pId = 2, Quantity = 10, Sno = 2 });
            context.CartProducts.Add(new CartProduct { CartId = 2, pId = 1, Quantity = 2, Sno = 2 });

            context.SaveChanges();
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }
       
        [Test]
        [Order(1)]
        public void DUpdateProductTest()
        {
            var cartData = context.CartProducts.Find(2);
            cartData.Quantity = 5;
            cartRepo.DecQuantity(2,2,1);
            Assert.AreEqual(cartData.Quantity, 4);
        }

        [Test]
        [Order(2)]
        public void IUpdateProductTest()
        {
            var cartData = context.CartProducts.Find(1);
            cartRepo.IncQuantity(1, 1,1);
            Assert.AreEqual(cartData.Quantity, 21);
        }

        //[Test]
        //[Order(3)]
        //public void DeleteProductTest()
        //{
        //    var cart = context.CartProducts.Find(2);
        //    cartRepo.RemovefromCart(2);
        //    Assert.AreEqual(cart,null);
        //}

        //[Test]
        //[Order(3)]
        //public void AddProductTest()
        //{
        //    var cart = context.CartProducts.Find(1);
        //    cartRepo.AddToCart();
        //    Assert.AreEqual(cart.Quantity, 21);
        //}

    }
}