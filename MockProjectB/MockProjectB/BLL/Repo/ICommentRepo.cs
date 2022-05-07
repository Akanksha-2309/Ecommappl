using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repo
{
    public interface ICommentRepo
    {

        public List<CommentInfo> GetCommentsbyProduct(int Pid);
        public ResponseMessage AddComment(Comment comments);
        public ResponseMessage UpdateComment(Comment comments);
        public ResponseMessage RemoveComment(int cmtid);

    }
}
