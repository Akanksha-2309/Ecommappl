using DAL;
using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repo
{
    public class CartRepo : ICartRepository
    {
        private readonly DataBaseContext _dbcontext;
        public readonly IOrderRepo _orderRepo;

        /// <summary>
        /// Constructor for using Database context 
        /// </summary>
        /// <param name="dbcontext"></param>
        public CartRepo(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        
        }

        public ResponseMessage AddToCart(CartProduct cartproduct, int uid)
        {
            int quantity = 1;
            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            Products product = _dbcontext.Productss.FirstOrDefault(x => x.pId == cartproduct.pId);
            CartProduct existingCartProduct1 = _dbcontext.CartProducts.FirstOrDefault(x => x.CartId == cartproduct.CartId && x.pId == cartproduct.pId);
            if (user.Role == "Customer")
            {
                if (existingCartProduct1 != null)
                {
                    if (product.Status == "Active")
                    {
                        existingCartProduct1.Quantity += 1;
                        try
                        {
                            _dbcontext.Entry(existingCartProduct1).State = EntityState.Modified;
                            _dbcontext.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        return new ResponseMessage { Message = "Product added successfully" };
                    }
                    else
                    {
                        return new ResponseMessage { Message = "Product is out of stock" };
                    }
                }
                else
                {
                    CartProduct cartProducts = new CartProduct
                    {
                        CartId = cartproduct.CartId,
                        pId = cartproduct.pId,
                        Quantity = quantity
                    };
                    if (product.Status == "Active")
                    {
                        try
                        {
                            _dbcontext.CartProducts.Add(cartProducts);
                            _dbcontext.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        return new ResponseMessage { Message = "Product added successfully" };
                    }
                    else
                    {
                        return new ResponseMessage { Message = "Product is out of stock" };
                    }
                }
            }
            else
            {
                return new ResponseMessage { Message = "Only for customer access" };
            
            }
        }


       
        public ModelView GetCartProducts(int id)
        {
           
            List<ProductInfo> products= (from cartproduct in _dbcontext.CartProducts
                    join cart in _dbcontext.Carts on id equals cart.CartId
                    join user in _dbcontext.Users on cart.Id equals user.Id
                    join product in _dbcontext.Productss on cartproduct.pId equals product.pId
                    where cartproduct.CartId == id //&& user.Role=="Customer"
                    select new ProductInfo
                    {
                        ProductID=product.pId,
                        PQuantity = cartproduct.Quantity,
                        ProductName = product.pName,
                        ProductPrice = product.Price
                        
                    }).ToList();
            double res= 0;
            foreach (var x in products)
            {
                res=res+(x.PQuantity * x.ProductPrice);
            }
           
            return (from cart in _dbcontext.Carts
             join cartproduct in _dbcontext.CartProducts on cart.CartId equals cartproduct.CartId
             join user in _dbcontext.Users on cart.Id equals user.Id
             join product in _dbcontext.Productss on cartproduct.pId equals product.pId
             where cart.CartId == id //&& user.Role == "Customer"
                    select new ModelView
             {
                 UserName=user.Name,
                 CartID=cart.CartId,
                 Products=products,
                 TotalPrice=res

             }).FirstOrDefault();
            
          
        }

        public ResponseMessage RemovefromCart(int Cid)
        {
            //User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            //if(user.Role=="Customer")
            //{
                var cartP = _dbcontext.Carts.Find(Cid);
                List<CartProduct> list = _dbcontext.CartProducts.Where(s => s.CartId.Equals(Cid)).ToList();
                _dbcontext.CartProducts.RemoveRange(list);
                _dbcontext.SaveChanges();
                return new ResponseMessage { Message = "All Products from Cart Removed Successfully" };
            //}
            //else
            //{
            //    return new ResponseMessage { Message = "Only for Customer Access" };
            //}
            

        }

        public ResponseMessage RemoveProductfromCart(int Cid, int Pid, int uid)
        {
            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            if (user.Role == "Customer")
            {
                CartProduct cp = _dbcontext.CartProducts.Where(s => s.CartId.Equals(Cid) && s.pId.Equals(Pid)).FirstOrDefault();
                _dbcontext.CartProducts.Remove(cp);
                _dbcontext.SaveChanges();
                return new ResponseMessage { Message = "Product from Cart Removed Successfully" };
            }
            else
            {
                return new ResponseMessage { Message = "Only for Customer Access" };
            }

        }

        public ResponseMessage DecQuantity(int Cid, int Pid, int uid)
        {
            CartProduct cp = _dbcontext.CartProducts.Where(s => s.CartId.Equals(Cid) && s.pId.Equals(Pid)).FirstOrDefault();
            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            if(user.Role=="Customer")
            {
                if (cp.Quantity == 1)
                {
                    RemoveProductfromCart(Cid, Pid,uid);
                }
                else
                {
                    cp.Quantity--;
                    try
                    {
                        _dbcontext.CartProducts.Update(cp);
                        _dbcontext.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }
                }
                return new ResponseMessage { Message = "Quantity has been decreased" };
            }
            else
            {
                return new ResponseMessage { Message = "Only for Customer Access" };
            }



        }
        public ResponseMessage IncQuantity(int Cid, int Pid, int uid)
        {
            CartProduct cp = _dbcontext.CartProducts.Where(s => s.CartId.Equals(Cid) && s.pId.Equals(Pid)).FirstOrDefault();
            User user = _dbcontext.Users.FirstOrDefault(x => x.Id == uid);
            if (user.Role == "Customer")
            {
                cp.Quantity++;
                try
                {
                    _dbcontext.CartProducts.Update(cp);
                    _dbcontext.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return new ResponseMessage { Message = "Quantity has been increased" };
            }
                
                

            else
            {
                return new ResponseMessage { Message = "Only for Customer Access" };
            }
        }

        public double GetTotalPrice(int Cid)
        {
            var a=GetCartProducts(Cid);
            double res = 0;
            foreach (var x in a.Products)
            {
                res = res + (x.PQuantity * x.ProductPrice);
            }
            return res;
        }

        
        IOrderRepo orderRepo;
        public ResponseMessage PaymentProcess(int Cid, double payment)
        {
            orderRepo = new OrderRepo(_dbcontext);
            double x;
            x = GetTotalPrice(Cid);

            var user = (from cart in _dbcontext.Carts
                        join users in _dbcontext.Users on cart.Id equals users.Id
                        where cart.CartId == Cid
                        select users).FirstOrDefault();
            List<Products> product = (from cartproduct in _dbcontext.CartProducts
                        join products in _dbcontext.Productss on cartproduct.pId equals products.pId
                        where cartproduct.CartId == Cid
                        select products).ToList();
            List<CartProduct> cartp = (from cartproduct in _dbcontext.CartProducts
                           join products in _dbcontext.Productss on cartproduct.pId equals products.pId
                           where cartproduct.CartId == Cid
                           select cartproduct).ToList();

            if (x == payment)
            {
            

                orderRepo.AddOrder(Cid);
                int i = 0;
                foreach (var p in product)
                {

                    p.Quantity = p.Quantity - cartp[i].Quantity;
                    i++;
                }
                RemovefromCart(Cid);
               
                    return new ResponseMessage
                    {
                        Message = "Transaction is successful with amount " + payment+ " "+
                        "and the order is Placed at " + 
                        user.Address + ", " +
                        user.City + ", " +
                        user.State + " at PINCODE = " + 
                        user.Pincode + " " +
                        "linked with contact number " + 
                        user.ContactNumber + "." + " "+
                        "Thankyou "+user.Name+", "+" Continue shopping!"
                    };
                
            }
            else
            {
                return new ResponseMessage { Message = "Transaction is not successful, Please try again with correct amount!" };
            }
        }
    }
}
