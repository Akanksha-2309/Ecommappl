using BLL.FactoryRepo;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace ECommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repo;
        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Products>> GetProducts()
        {
            return _repo.GetProducts();
        }

        [HttpGet()]

        [Route("byProductName/{name}")]

        public List<Products> GetProductsByName(string name)
        {
            return _repo.GetProductsByName(name);
        }

        [HttpPost("{uid}")]
        public ResponseMessage AddProducts([FromBody] List<Products> products, int uid)
        {
            return _repo.AddProduct(products, uid);
            
        }

        [HttpGet()]
        [Route("byStatus/{status}")]
        public List<Products> GetProductsByStatus(string status)
        {
            return _repo.GetProductsByStatus(status);
        }


        [HttpPut("deletebyId/{id}/{uid}")]
        public ResponseMessage DeleteProduct(int id,int uid)
        {
            return _repo.DeleteProduct(id,uid);
       
        }//delete a product by id

        [HttpPut("{uid}")]
        public ResponseMessage UpdateProduct([FromBody] Products product, int uid)
        {
            return _repo.UpdateProduct(product, uid);

            
        }


        [HttpGet("{id}")]
        public List<ProductView> GetProductByID(int id)
        {
            return _repo.GetProductByID(id);
        }

        [HttpGet("byCategoryName/{name}")]
        public List<ProductView> GetProductsByCategoryName(string name)
        {
            return _repo.GetProductsByCategoryName(name);
        }
        [HttpGet("byCategoryType/{name}")]
        public List<ProductView> GetProductsByCategoryType(string name)
        {
            return _repo.GetProductsByCategoryType(name);
        }
    }
}
