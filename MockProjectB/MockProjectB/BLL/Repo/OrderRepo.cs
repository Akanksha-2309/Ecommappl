using DAL;
using DAL.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly DataBaseContext _dbcontext;

        public OrderRepo(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
       
        public OrderView GetOrderList(int Oid)
        {

            List<OrderInfo> orders = (from orderdetails in _dbcontext.OrderDetailss
                                          join order in _dbcontext.Orders on Oid equals order.OId
                                          join cart in _dbcontext.Carts on order.CartId equals cart.Id
                                          join user in _dbcontext.Users on cart.Id equals user.Id
                                          join product in _dbcontext.Productss on orderdetails.ProductId equals product.pId
                                          where orderdetails.OId == Oid
                                          select new OrderInfo
                                          {
                                              PQuantity = orderdetails.Quantity,
                                              ProductName = product.pName,
                                              ProductPrice = product.Price

                                          }).ToList();
            

            return (from orderdetails in _dbcontext.OrderDetailss
                    join order in _dbcontext.Orders on Oid equals order.OId
                    join cart in _dbcontext.Carts on order.CartId equals cart.Id
                    join user in _dbcontext.Users on cart.Id equals user.Id
                    join product in _dbcontext.Productss on orderdetails.ProductId equals product.pId
                    where orderdetails.OId == Oid
                    select new OrderView
                    {
                        UserName = user.Name,
                        OrderID= orderdetails.OId,
                        Orders= orders

                    }).FirstOrDefault();


        }

        public void AddOrder(int Cid)
        {

            Order order = new Order();
            order.CartId = Cid;
            try
            {
                _dbcontext.Orders.Add(order);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            var str = from a in _dbcontext.CartProducts where a.CartId == Cid select a;
            IList<OrderDetails> orders = new List<OrderDetails>();
            foreach (var val in str)
            {
                OrderDetails o = new OrderDetails();

                o.OId = order.OId;
                o.ProductId = val.pId;
                o.Quantity = val.Quantity;

                orders.Add(o);
            }
            try
            {

                _dbcontext.OrderDetailss.AddRange(orders);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

