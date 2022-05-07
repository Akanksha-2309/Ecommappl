using BLL.Repo;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _repo;
        public CommentController(ICommentRepo repo)
        {

            _repo = repo;
        }
  
       

        // GET api/<CommentsController>/5
        
        [HttpGet("{Pid}")]
        public List<CommentInfo> GetCommentsbyProduct(int Pid)
        {

            return _repo.GetCommentsbyProduct(Pid);
        }
        // POST api/<CommentsController>
        [HttpPost]
        public ResponseMessage AddComment(Comment comments)
        {

            return _repo.AddComment(comments);
        }
        

        // PUT api/<CommentsController>/5
        [HttpPut]
        public ResponseMessage UpdateCategory(Comment comments)
        {

            return _repo.UpdateComment(comments);
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{cmtid}")]
        public ResponseMessage DeleteComment(int cmtid)
        {
            return _repo.RemoveComment(cmtid);
        }
    }
}
