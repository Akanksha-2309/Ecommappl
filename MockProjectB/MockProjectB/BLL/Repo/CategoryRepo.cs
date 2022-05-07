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

namespace BLL.Repo
{
    public class CategoryRepo: ICategoryRepo
    {
        private readonly DataBaseContext _dbcontext;

        public CategoryRepo(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //adding product
        public ResponseMessage AddCategory(List<Category> categories, int uid)
        {
            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            if(user.Role=="Admin")
            {
                try
                {
                    categories.ForEach(categories => _dbcontext.Categories.Add(categories));
                    _dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return new ResponseMessage { Message = "New Category(s) Added Successfully" };
            }
            else
            {
                return new ResponseMessage { Message = "Only for admin access" };
            }
            

        }


        //deleting product by id
        public ResponseMessage DeleteCategory(int id,int uid)
        {

            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid );
            var category = _dbcontext.Categories.Find(id);
            if (category != null && user.Role=="Admin")
            {
                try
                {
                    _dbcontext.Categories.Remove(category);
                    _dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return new ResponseMessage { Message = "Category Removed Successfully" };
            }
            else
                return new ResponseMessage { Message = "No Record Found/Only for Admin Access" };
        }


        //list all the product in the inventory
        public List<Category> GetCategories()
        {
            return (from u in _dbcontext.Categories select u).ToList();

        }

        //get cat by id
        public Category GetCategoryById(int id)
        {

            return (_dbcontext.Categories.Find(id));

        }

        //get category by name 
        public List<Category> GetCategoryByName(string name)
        {
            return _dbcontext.Categories.Where(x => x.cName.Equals(name)).ToList();
        }

        //updates
        public ResponseMessage UpdateCategory(Category category, int uid)
        {
            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            if(user.Role=="Admin")
            {
                try
                {
                    _dbcontext.Categories.Update(category);
                    _dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return new ResponseMessage { Message = "Category Details Updated Successfully" };
            }
            else
            {
                return new ResponseMessage { Message = "Only for admin access" };
            }
        }


    }
}
