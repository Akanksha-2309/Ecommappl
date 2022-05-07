using BLL.FactoryRepo;
using BLL.Repo;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ECommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return _repo.GetCategories();
        }

        [HttpGet()]

        [Route("byCategoryName/{name}")]

        public List<Category> GetCategoryByName(string name)
        {
            return _repo.GetCategoryByName(name);
        }

        [HttpPost("{uid}")]
        public ResponseMessage AddCategory(List<Category> categories, int uid)
        {
            return _repo.AddCategory(categories,uid);

        }


        [HttpPut("{uid}")]
        public ResponseMessage UpdateCategory([FromBody] Category category,int uid)
        {
           return _repo.UpdateCategory(category,uid);
        }


        [HttpGet("{id}")]
        public Category GetCategoryById(int id)
        {
            return _repo.GetCategoryById(id);
        }

        [HttpDelete("{id}/{uid}")]
        public ResponseMessage DeleteCategory(int id,int uid)
        {
             return _repo.DeleteCategory(id,uid);
        }

    }
}
