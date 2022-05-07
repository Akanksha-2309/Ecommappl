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
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repo;
        public CartController(ICartRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("{uid}")]
        public ResponseMessage AddToCart([FromBody] CartProduct Cid, int uid)
        {
            return _repo.AddToCart(Cid, uid);

        }
        [HttpGet("{id}")]
        public ModelView GetCartProducts(int id)
        {
            return _repo.GetCartProducts(id);
        }
        [HttpDelete("{Cid}")]
        public ResponseMessage RemovefromCart( int Cid)
        {
             return _repo.RemovefromCart(Cid);
         
        }
        [HttpDelete("{Cid}/{Pid}/{uid}")]
        public ResponseMessage RemoveProductfromCart(int Cid,int Pid, int uid)
        {
            return _repo.RemoveProductfromCart(Cid,Pid,uid);
           
        }
        [HttpPut("DecQuantity/{Cid}/{Pid}/{uid}")]

        public ResponseMessage DecQuantity(int Cid, int Pid, int uid)
        {
            return _repo.DecQuantity(Cid, Pid, uid);
        }
        [HttpPut("IncQuantity/{Cid}/{Pid}/{uid}")]

        public ResponseMessage IncQuantity(int Cid, int Pid, int uid)
        {
            return _repo.IncQuantity(Cid, Pid, uid );
        }

        //[HttpGet("getPrice/{Cid}")]
        //public double GetTotalPrice(int Cid)
        //{
        //    return _repo.GetTotalPrice(Cid);
        //}
        [HttpGet("Pay/{Cid}/{Payment}")]
        public ResponseMessage PaymentProcess(int Cid, double Payment)
        {
            return _repo.PaymentProcess(Cid, Payment);
        }

    }
}
