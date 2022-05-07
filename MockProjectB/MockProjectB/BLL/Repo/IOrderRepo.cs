using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repo
{
    public interface IOrderRepo
    {
        public void AddOrder(int Cid);
        public OrderView GetOrderList(int Oid);

    }
}
