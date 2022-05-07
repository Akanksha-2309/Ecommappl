using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repo
{
    public interface ICartRepository
    {
        public ResponseMessage AddToCart(CartProduct cartproduct, int uid);
        public ResponseMessage RemovefromCart(int Cid);
        public ResponseMessage RemoveProductfromCart(int Cid, int Pid, int uid);
        public ResponseMessage DecQuantity(int Cid, int Pid,int uid);
        public ResponseMessage IncQuantity(int Cid, int Pid, int uid);
        public ModelView GetCartProducts(int Cid);
        public double GetTotalPrice(int Cid);
        public ResponseMessage PaymentProcess(int Cid, double payment);
        //public object GetUsersDetails(int Cid);


    }
}
