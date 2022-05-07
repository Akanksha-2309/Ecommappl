using BLL.FactoryRepo;
using DAL;
using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BLL.ProductRepo
{
    public class ProductRepo : IProductRepo
    {
        private readonly DataBaseContext _dbcontext;

        public ProductRepo(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
   

        public ResponseMessage AddProduct(List<Products> products, int uid)
        {
            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            if(user.Role=="Admin")
            {
                try
                {

                    products.ForEach(product => _dbcontext.Productss.Add(product));
                    _dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return new ResponseMessage { Message = "New Product(s) Added Successfully" };
            }
            else
            {
                return new ResponseMessage { Message = "Only for admin access" };
            }
            

        }

        public ResponseMessage DeleteProduct(int id,int uid)
        {
            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            var query =
            (from p in _dbcontext.Productss
             where p.pId == id && user.Role=="Admin"
             select p).FirstOrDefault();

            try
            {
                query.Status = "Inactive";
                _dbcontext.Productss.Update(query);
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new ResponseMessage { Message = "Product has been set to inactive" };

        }


        public List<Products> GetProducts()
        {
            return (from u in _dbcontext.Productss select u).ToList();

        }

        public List<ProductView> GetProductByID(int id)
        {
            return (from product in _dbcontext.Productss
                    join category in _dbcontext.Categories on product.cId equals category.cId
                    where product.pId == id
                    select new ProductView
                    {
                       ProductId= product.pId,
                        //CategoryId=  category.cId,
                        ProductName = product.pName,
                        CategoryName = category.cName,
                        PQuantity= product.Quantity,
                        ProductPrice= product.Price,
                        PDescription= product.Description,
                        PStatus= product.Status,
                        PDateofCreation= product.DateofCreation,
                        
                    }).ToList();
       
        }

        public List<ProductView> GetProductsByCategoryName(string name)
        {
            return (from product in _dbcontext.Productss
                    join category in _dbcontext.Categories on product.cId equals category.cId
                    where category.cName == name
                    select new ProductView
                    {
                         ProductId= product.pId,
                         CategoryName=  category.cName,
                         ProductName=product.pName,
                         PQuantity=product.Quantity,
                         ProductPrice=product.Price,
                         PDescription=product.Description,
                         PStatus = product.Status,
                         PDateofCreation= product.DateofCreation,
                    }).ToList();
        }

        public List<ProductView> GetProductsByCategoryType(string name)
        {
            return (from product in _dbcontext.Productss
                    join category in _dbcontext.Categories on product.cId equals category.cId
                    where category.Type == name
                    select new ProductView
                    {
                        ProductId = product.pId,
                        CategoryName = category.cName,
                        ProductName = product.pName,
                        PQuantity = product.Quantity,
                        ProductPrice = product.Price,
                        PDescription = product.Description,
                        PStatus= product.Status,
                        PDateofCreation = product.DateofCreation,
                    }).ToList();
        }

        public Products GetProductsById(int id)
        {
            return (_dbcontext.Productss.Find(id));

        }
        public List<Products> GetProductsByName(string name)
        {
            return _dbcontext.Productss.Where(x => x.pName == name).ToList();
        }
        public List<Products> GetProductsByStatus(string status)
        {
            return _dbcontext.Productss.Where(i => i.Status.Equals(status)).ToList();
        }




        public ResponseMessage UpdateProduct(Products product, int uid)
        {
            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            if (user.Role=="Admin")
            {
                try
                {
                    _dbcontext.Productss.Update(product);
                    _dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return new ResponseMessage { Message = "Product updated successfully " };
            }
            
            else
            {
                return new ResponseMessage { Message = "Only for admin access" };
            }

        }

    }
}
