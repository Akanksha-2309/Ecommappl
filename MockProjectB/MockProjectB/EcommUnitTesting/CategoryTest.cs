using BLL.Repo;
using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace EcommUnitTesting
{
    public class Tests
    {
        DataBaseContext context;
        ICategoryRepo categoryRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
           .UseInMemoryDatabase(databaseName: "MyApp")
           .Options;
            context = new DataBaseContext(options);
            // Insert seed data into the database using one instance of the context
            categoryRepo = new CategoryRepo(context);
            context.Categories.Add(new Category { cId = 1, cName = "C1", Type = "Type1 " });
            context.Categories.Add(new Category { cId = 2, cName = "C2", Type = "Type2 " });
            context.Users.Add(new User { Id = 1, Name = "Atiksha", ContactNumber = 9466287901, Email = "a@gmail.com", Role = "Customer", Address = "ADD1", City = "c1", Pincode = 282002, State = "S1" });
            context.Users.Add(new User { Id = 2, Name = "Akanksha", ContactNumber = 9466287111, Email = "aka@gmail.com", Role = "Admin", Address = "ADD2", City = "c2", Pincode = 282002, State = "S2" });



            context.SaveChanges();

        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }

        [Test]
        [Order(1)]
        public void GetCategoryTest()
        {
            var category = categoryRepo.GetCategories();
            Assert.AreEqual(category.Count, 2);
        }


        [Test]
        [Order(2)]
        public void UpdateCategoryTest()
        {

            var category = context.Categories.Find(1);
            category.cName = "C3";
            categoryRepo.UpdateCategory(category,2);
            Assert.AreEqual(category.cName, "C3");
        }
        [Test]
        [Order(3)]
        public void AddCategoryTest()
        {
            var categories = new Category();
            categories.cId = 3;
            categories.cName = "c3";
            categories.Type = "type 3";
            
            List<Category> category1 = new List<Category>();
            category1.Add(categories);
            categoryRepo.AddCategory(category1,2);
            var category2 = categoryRepo.GetCategories();
            Assert.AreEqual(category2.Count, 3);



        }
        [Test]
        [Order(4)]
        public void DeleteCategoryTest()
        {
          
            
            categoryRepo.DeleteCategory(2,2);
            var category = categoryRepo.GetCategories();
            Assert.AreEqual(category.Count, 2);

        }

        
    }
}