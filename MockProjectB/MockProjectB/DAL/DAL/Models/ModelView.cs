using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ModelView
    {
        public int CartID { get; set; }

        public string UserName { get; set; }
        
        public List<ProductInfo> Products { get; set; }
        public double TotalPrice { get; set; } = 0;



    }
    //public class CommentView
    //{
    //    public int Productid { get; set; }
    //    public string ProductName { get; set; }
    //    public List<CommentInfo> Commentinfo { get; set; }

    //}
    
  
    public class CommentInfo
    {
       
        public int Commentid { get; set; }
        public int Productid { get; set; }
    
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        public int Parentid { get; set; }
        public List<CommentInfo> Replyinfo { get; set; }


    }
    public class OrderView
    {
        public int OrderID { get; set; }

        public string UserName { get; set; }

        public List<OrderInfo> Orders { get; set; }

    }

    public class ProductInfo
    {
        public int ProductID { get; set; }
        public int PQuantity { get; set; }

        public string ProductName { get; set; }
        public float ProductPrice { get; set; }


    }
    public class ProductView
    {

        public int ProductId { get; set; }
       // public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string ProductName { get; set;}

        public int PQuantity { get; set; }
        public string PDescription { get; set; }
        public float ProductPrice { get; set; }
        public string PStatus { get; set; }

        public DateTime PDateofCreation { get; set; }


    }
    public class OrderInfo
    {

        public string ProductName { get; set; }

        public int PQuantity { get; set; }

        
        public float ProductPrice { get; set; }


    }
}
