using BLL.Repo;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _repo;
        public OrderController(IOrderRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("{Oid}")]
        public OrderView GetOrderList(int Oid)
        {
            return _repo.GetOrderList(Oid);
        }

    }
}
