using BLL.Repo;
using BusinessLayer.Repositories;
using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;namespace EcommUnitTesting
{
    public class CommentTest
    {
        DataBaseContext context;
        ICommentRepo commentRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "MyApp")
            .Options;
            context = new DataBaseContext(options);
            // Insert seed data into the database using one instance of the context
            commentRepo = new CommentRepo(context);
            context.Comments.Add(new Comment { CmtId = 1, Date = System.DateTime.Now, Comments = "C1", ParentId = 0, ProductId = 1, UserId = 1 });
            context.Comments.Add(new Comment { CmtId = 2, Date = System.DateTime.Now, Comments = "C2", ParentId = 1, ProductId = 1, UserId = 2 });
            context.Comments.Add(new Comment { CmtId = 4, Date = System.DateTime.Now, Comments = "C4", ParentId = 0, ProductId = 1, UserId = 2 }); context.SaveChanges();
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }
      
        [Test]
        [Order(1)]
        public void GetCommentByProductTest()
        {
            var category = commentRepo.GetCommentsbyProduct(1);
            Assert.AreEqual(category.Count, 2);
        }
        [Test]
        [Order(2)]
        public void UpdateCommentTest()
        {
            var comment = context.Comments.Find(1);
            comment.Comments = "Comment Updated";
            commentRepo.UpdateComment(comment);
            Assert.AreEqual(comment.Comments, "Comment Updated");
        }
        [Test]
        [Order(3)]
        public void AddCategoryTest()
        {
            var comments = new Comment();
            comments.CmtId = 3;
            comments.Date = System.DateTime.Now;
            comments.Comments = "C3";
            comments.ParentId = 2;
            comments.ProductId = 1;
            comments.UserId = 1;
            commentRepo.AddComment(comments);
            var comment2 = commentRepo.GetCommentsbyProduct(1);
            Assert.AreEqual(comment2.Count, 2);
        }
        [Test]
        [Order(4)]
        public void DeleteCommentTest()
        {
            commentRepo.RemoveComment(1);
            var comment = commentRepo.GetCommentsbyProduct(1);
            Assert.AreEqual(comment.Count, 1);
        }
    }
}


