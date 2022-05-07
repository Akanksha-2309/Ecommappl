using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailsId { get; set; }
        [Display(Name = "Order")]
        public int OId { get; set; }
        [ForeignKey("OId")]
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}





