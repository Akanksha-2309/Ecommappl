using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BLL.FactoryRepo
{
    public interface IProductRepo
    {
        public List<Products> GetProducts();
        public Products GetProductsById(int id);
        public List<Products> GetProductsByStatus(string status);
        public List<Products> GetProductsByName(string name);
        public ResponseMessage AddProduct(List<Products> products,int uid);
        public ResponseMessage UpdateProduct(Products product, int uid);
        public ResponseMessage DeleteProduct(int id,int uid);
        public List<ProductView> GetProductByID(int id);
        public List<ProductView> GetProductsByCategoryName(string name);
        public List<ProductView> GetProductsByCategoryType(string name);
    }
}
