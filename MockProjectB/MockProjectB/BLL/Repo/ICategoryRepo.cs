using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repo
{
    public interface ICategoryRepo
    {
        public Category GetCategoryById(int id);
        public List<Category> GetCategoryByName(string name);
        public List<Category> GetCategories();
        public ResponseMessage UpdateCategory(Category category,int uid);

        public ResponseMessage AddCategory(List<Category> categories,int uid);

        public ResponseMessage DeleteCategory(int id, int uid);
    }
}
