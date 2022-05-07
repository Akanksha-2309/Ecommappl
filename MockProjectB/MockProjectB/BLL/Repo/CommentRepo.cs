using BLL.Repo;
using DAL;
using DAL.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class CommentRepo : ICommentRepo
    {
        private readonly DataBaseContext _dbcontext;
        public CommentRepo(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
       
        public List<CommentInfo> GetCommentsbyProduct(int Pid)
        {

            List<Comment> comments = _dbcontext.Comments.ToList();
            return comments
                .Where(c => c.ParentId == 0 && c.ProductId == Pid)
                .Select(c => new CommentInfo
                {
                    Commentid = c.CmtId,
                     Productid = c.ProductId,
                    Date = c.Date,
                    Parentid = c.ParentId,
                    CommentText = c.Comments,
                    Replyinfo = GetReply(comments, c.CmtId)
                }).ToList();

        }
        public List<CommentInfo> GetReply(List<Comment> comments,int parentId )
        {
            return comments
                .Where(c=>c.ParentId == parentId )
                .Select(c => new CommentInfo
                {

                    Commentid = c.CmtId,
                     Productid= c.ProductId,
                    Date = c.Date,
                    Parentid = c.ParentId,
                    CommentText = c.Comments,
                    Replyinfo = GetReply(comments, c.CmtId)



                }).ToList();
        }
       
        public ResponseMessage AddComment(Comment comments)
        {
            var id = (from u in _dbcontext.Users
                      where u.Id == comments.UserId
                      select u.Id).SingleOrDefault();
            var Iid = (from u in _dbcontext.Productss
                       where u.pId == comments.ProductId
                       select u.pId).SingleOrDefault();
            if (comments.UserId == id)
            {
                if (comments.ProductId == Iid)
                {
                    try
                    {
                        _dbcontext.Comments.Add(comments);
                        _dbcontext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return new ResponseMessage { Message = "Comment Added Successfully" };
                }
                else
                {
                    return new ResponseMessage { Message = "Product id not found" };
                }
            }
            else
            {
                return new ResponseMessage { Message = "User id not found" };
            }
        }
      
        public ResponseMessage UpdateComment(Comment comments)
        {
            try
            {
                _dbcontext.Comments.Update(comments);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new ResponseMessage { Message = "Comment Updated Successfully" };

        }
        public ResponseMessage RemoveComment(int cmtid)
        {
            Comment comments = _dbcontext.Comments.Where(x => x.CmtId == cmtid  || x.ParentId==cmtid).FirstOrDefault();
            try
            {

                _dbcontext.Comments.Remove(comments);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new ResponseMessage { Message = "Comment Deleted Successfully!" };
        }
    }
}

